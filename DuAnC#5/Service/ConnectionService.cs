using BUS.Interface;

namespace DuAnC_5.Service
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConfiguration _configuration;
        public ConnectionService(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public string? Datebase => _configuration.GetConnectionString("Db");
    }
}
