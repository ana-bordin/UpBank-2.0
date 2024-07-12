using System.Data;

namespace UPBank.Customer.Infra.Context
{
    public interface IUpBankApiCustomerContext
    {
        public IDbConnection ConnectionCustomer { get; }
    }
}
