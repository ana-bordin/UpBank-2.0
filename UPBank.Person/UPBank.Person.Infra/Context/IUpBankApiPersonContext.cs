using System.Data;

namespace UPBank.Person.Infra.Context
{
    public interface IUpBankApiPersonContext : IDisposable
    {
        public IDbConnection Connection { get; }
    }
}
