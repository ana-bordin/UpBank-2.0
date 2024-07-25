using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace UPBank.Account.Infra.Context
{
    public class UpBankApiAccountContext : IUpBankApiAccountContext
    {
        private readonly IConfiguration _configuration;
        public UpBankApiAccountContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUpBankApiAccountContext.ConnectionAccount
        {
            get
            {
                var connectionString = _configuration.GetSection("ConnectionStrings:UpBankApiAccountContext").Value;
                return new SqlConnection(connectionString);
            }
        }
    }
}
