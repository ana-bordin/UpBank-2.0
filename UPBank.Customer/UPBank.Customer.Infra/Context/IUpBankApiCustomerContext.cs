using System.Data;

namespace UPBank.Customer.Infra.Context
{
    public interface IUpBankApiCustomerContext : IDisposable
    {
        public IDbConnection Connection{ get; }
    }
}