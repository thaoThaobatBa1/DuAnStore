using BUS.Base;
using BUS.Features.Merchants.Dtos;
using BUS.Interface;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PaymentApplication.Constants;
using PaymentApplication.Features.Payment.Dtos;
using PaymentService.Momo.Config;
using PaymentService.Momo.Request;
using PaymentUltils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplication.Features.Payment.Commands
{
    public class ProcessMomoPaymentReturn : MomoOneTimePaymentResultRequest,
        IRequest<BaseResultWithData<(PaymentReturnDtos, string)>>
    {
    }
    public class ProcessMomoPaymentReturnHandler
        : IRequestHandler<ProcessMomoPaymentReturn, BaseResultWithData<(PaymentReturnDtos, string)>>
    {
        private readonly IConnectionService connectionService;
        private readonly ISqlService sqlService;
        private readonly MomoConfig momoConfig;

        public ProcessMomoPaymentReturnHandler(IConnectionService connectionService,
            ISqlService sqlService,
            IOptions<MomoConfig> momoConfigOptions)
        {
            this.connectionService = connectionService;
            this.sqlService = sqlService;
            this.momoConfig = momoConfigOptions.Value;
        }

        public Task<BaseResultWithData<(PaymentReturnDtos, string)>> Handle(ProcessMomoPaymentReturn request, CancellationToken cancellationToken)
        {
            string returnUrl = string.Empty;
            var result = new BaseResultWithData<(PaymentReturnDtos, string)>();

            try
            {
                var resultData = new PaymentReturnDtos();
                var isValidSignature = request.IsValidSignature(momoConfig.AccessKey, momoConfig.SecretKey);

                if (isValidSignature)
                {
                    string connectionString = connectionService.Datebase ?? string.Empty;
                    var paramters = new SqlParameter[]
                    {
                            new SqlParameter("@PaymentId", request.orderId),
                    };
                    (var data, string sqlError) = sqlService.FillDataTable(connectionString,
                        PaymentConstants.SelectByIdSprocName, paramters);
                    var payment = data.AsListObject<PaymentDtos>()?.SingleOrDefault();

                    if (payment != null)
                    {
                        paramters = new SqlParameter[]
                        {
                            new SqlParameter("@Id", payment.MerchantId),
                        };
                        (data, sqlError) = sqlService.FillDataTable(connectionString,
                            MerchantContants.SelectByIdSprocName, paramters);
                        var merchant = data.AsListObject<MerchantDtos>()?.SingleOrDefault();
                        returnUrl = merchant?.MerchantReturnUrl ?? string.Empty;

                        if (request.resultCode == 0)
                        {
                            resultData.PaymentStatus = "00";
                            resultData.PaymentId = payment.Id;
                            ///TODO: Make signature
                            resultData.Signature = Guid.NewGuid().ToString();
                        }
                        else
                        {
                            resultData.PaymentStatus = "10";
                            resultData.PaymentMessage = "Payment process failed";
                        }

                        result.Success = true;
                        result.Message = MessageContants.OK;
                        result.Data = (resultData, returnUrl);
                    }
                    else
                    {
                        resultData.PaymentStatus = "11";
                        resultData.PaymentMessage = "Can't find payment at payment service";
                    }
                }
                else
                {
                    resultData.PaymentStatus = "99";
                    resultData.PaymentMessage = "Invalid signature in response";
                }
            }
            catch (Exception ex)
            {
                result.Set(false, MessageContants.Error);
                result.Errors.Add(new BaseError()
                {
                    Code = MessageContants.Exception,
                    Message = ex.Message,
                });
            }

            return Task.FromResult(result);

        }
    }
}