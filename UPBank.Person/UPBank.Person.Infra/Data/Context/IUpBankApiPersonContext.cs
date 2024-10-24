using System.Data;

namespace UPBank.Person.Infra.Data.Context
{
    public interface IUpBankApiPersonContext : IDisposable
    {
        public IDbConnection Connection { get; }
    }
}
