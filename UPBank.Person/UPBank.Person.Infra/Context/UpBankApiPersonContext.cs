using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace UPBank.Person.Infra.Context
{
    public class UpBankApiPersonContext : IUpBankApiPersonContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public UpBankApiPersonContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUpBankApiPersonContext.Connection
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed)
                {
                    var connectionString = _configuration.GetSection("ConnectionStrings:UpBankApiPersonContext").Value;
                    _connection = new SqlConnection(connectionString);
                    _connection.Open();

                }
                return _connection;
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}