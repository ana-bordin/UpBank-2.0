using System.Data;

namespace UPBank.Customer.Infra.Data.Context.Customer
{
    public interface IUpBankApiCustomerContext : IDisposable
    {
        public IDbConnection Connection { get; }
    }
}