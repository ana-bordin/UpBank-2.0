using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UPBank.Customer.Infra.Context
{
    public class UpBankApiCustomerContext : IUpBankApiCustomerContext
    {
        private IDbConnection _connection;
        private readonly IConfiguration _configuration;
        public UpBankApiCustomerContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUpBankApiCustomerContext.Connection
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