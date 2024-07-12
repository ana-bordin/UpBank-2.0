using System.Data;

namespace UPBank.Person.Infra.Context
{
    public interface IUpBankApiPersonContext
    {
        public IDbConnection Connection { get; }
    }
}
