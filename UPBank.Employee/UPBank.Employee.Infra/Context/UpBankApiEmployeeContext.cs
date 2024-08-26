using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace UPBank.Employee.Infra.Context
{
    public class UpBankApiEmployeeContext : IUpBankApiEmployeeContext
    {
        private readonly IConfiguration _configuration;
        private DbConnection _connection;

        public UpBankApiEmployeeContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUpBankApiEmployeeContext.Connection
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed)
                {
                    var connectionString = _configuration.GetSection("ConnectionStrings:UpBankApiEmployeeContext").Value;
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
