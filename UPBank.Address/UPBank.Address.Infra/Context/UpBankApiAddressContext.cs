using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UPBank.Address.Infra.Context
{
    public class UpBankApiAddressContext : IUpBankApiAddressContext
    {
        private readonly IConfiguration _configuration;
        public UpBankApiAddressContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUpBankApiAddressContext.Connection
        {
            get
            {
                var connectionString = _configuration.GetSection("ConnectionStrings:UpBankApiAddressContext").Value;
                return new SqlConnection(connectionString);
            }
        }
    }
}
