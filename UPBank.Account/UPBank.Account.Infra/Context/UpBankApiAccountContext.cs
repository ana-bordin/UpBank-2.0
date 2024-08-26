using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace UPBank.Account.Infra.Context
{
    public class UpBankApiAccountContext : IUpBankApiAccountContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public UpBankApiAccountContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUpBankApiAccountContext.ConnectionAccount
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed)
                {
                    var connectionString = _configuration.GetSection("ConnectionStrings:UpBankApiAccountContext").Value;
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