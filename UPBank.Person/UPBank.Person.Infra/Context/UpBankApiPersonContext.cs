using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace UPBank.Person.Infra.Context
{
    public class UpBankApiPersonContext : IUpBankApiPersonContext
    {
        private readonly IConfiguration _configuration;

        public UpBankApiPersonContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUpBankApiPersonContext.Connection
        {
            get
            {
                var connectionString = _configuration.GetSection("ConnectionStrings:UpBankApiPersonContext").Value;
                return new SqlConnection(connectionString);
            }
        }
    }
}
