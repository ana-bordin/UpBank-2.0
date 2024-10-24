using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace UPBank.Employee.Infra.Data.Context.Person
{
    internal class UpBankApiPersonContext : IUPBankApiPersonContext
    {
        private readonly IConfiguration _configuration;
        private DbConnection _connection;

        public UpBankApiPersonContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUPBankApiPersonContext.Connection
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
            _connection?.Close();
        }
    }
}
