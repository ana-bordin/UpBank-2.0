using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UPBank.Address.Infra.Context
{
    public class UpBankApiAddressContext : IUpBankApiAddressContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public UpBankApiAddressContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                var connectionString = _configuration.GetSection("ConnectionStrings:UpBankApiAddressContext").Value;
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                return _connection;
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}