using BUS.Base;
using BUS.Features.Merchant.Commands;
using BUS.Features.Merchants.Dtos;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DuAnC_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMediator mediator;

        public MerchantController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Get merchants base on criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResultWithData<List<MerchantDtos>>), 200)]
        [ProducesResponseType(400)]
        public IActionResult Get(string criteria)
        {
            var response = new BaseResultWithData<List<MerchantDtos>>();
            return Ok(response);
        }

        /// <summary>
        /// Get merchants paging
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("with-paging")]
        [ProducesResponseType(typeof(BaseResultWithData<BasePagingData<MerchantDtos>>), 200)]
        public IActionResult GetPaging([FromQuery] BasePagingQuery query)
        {
            var response = new BaseResultWithData<BasePagingData<MerchantDtos>>();
            return Ok(response);
        }

        /// <summary>
        /// Get one merchant by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(BaseResultWithData<MerchantDtos>), 200)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetOne([FromRoute] string id)
        {
            var response = new BaseResultWithData<MerchantDtos>();
            return Ok(response);
        }

        /// <summary>
        /// Create merchant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        ///     POST /merchants
        ///     {
        ///         "MerchantName" : "Website bán hàng A",
        ///         "MerchantWebLink" : "https://webbanhang.com",
        ///         "MerchantIpnUrl" : "https://webbanhang.com/ipn",
        ///         "MerchantReturnUrl" : "https://webbanhang.com/payment/return"
        ///     }
        /// 
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultWithData<MerchantDtos>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMerchant request)
        {
            var response = new BaseResultWithData<MerchantDtos>();
            response = await mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Update merchant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(BaseResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Update(string id, [FromBody] UpdateMerchant request)
        {
            var response = new BaseResult();
            return Ok(response);
        }

        /// <summary>
        /// Set Active Flag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/set-active")]
        [ProducesResponseType(typeof(BaseResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult SetActive(string id, [FromBody] SetActiveMerchant request)
        {
            var response = new BaseResult();
            return Ok(response);
        }

        /// <summary>
        /// Delete merchant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(BaseResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Delete(string id)
        {
            var response = new BaseResult();
            return Ok(response);
        }
    }
}
