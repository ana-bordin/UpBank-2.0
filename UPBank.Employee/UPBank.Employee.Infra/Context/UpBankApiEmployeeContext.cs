using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace UPBank.Employee.Infra.Context
{
    public class UpBankApiEmployeeContext : IUpBankApiEmployeeContext
    {
        private readonly IConfiguration _configuration;

        public UpBankApiEmployeeContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUpBankApiEmployeeContext.Connection
        {
            get 
            {
                var connectionString = _configuration.GetSection("ConnectionStrings:UpBankApiEmployeeContext").Value;
                return new SqlConnection(connectionString);
            }
        }
    }
}
