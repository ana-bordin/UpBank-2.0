using System.Data;

namespace UPBank.Customer.Infra.Data.Context.Person
{
    public interface IUpBankApiPersonContext : IDisposable
    {
        public IDbConnection Connection { get; }
    }
}