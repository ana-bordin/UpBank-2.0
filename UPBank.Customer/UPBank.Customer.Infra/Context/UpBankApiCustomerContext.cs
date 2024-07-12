using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UPBank.Customer.Infra.Context
{
    public class UpBankApiCustomerContext : IUpBankApiCustomerContext
    {
        private readonly IConfiguration _configuration;
        public UpBankApiCustomerContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IDbConnection IUpBankApiCustomerContext.ConnectionCustomer
        {
            get
            {
                var connectionString = _configuration.GetSection("ConnectionStrings:UpBankApiCustomerContext").Value;
                return new SqlConnection(connectionString);
            }
        }
    }
}
