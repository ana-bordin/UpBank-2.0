using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UPBank.Agency.Infra.Context
{
    public class UpBankApiAgencyContext : IUpBankApiAgencyContext
    {
        private IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public UpBankApiAgencyContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUpBankApiAgencyContext.Connection
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed)
                {
                    var connectionString = _configuration.GetSection("ConnectionStrings:UpBankApiCustomerContext").Value;
                    _connection = new SqlConnection(connectionString);
                    _connection.Open();

                }
                return _connection;
            }
        }

        public void Dispose()
        {
            _connection?.Close();
        }
    }
}