using System.Data;

namespace UPBank.Employee.Infra.Data.Context.Person
{
    public interface IUPBankApiPersonContext : IDisposable
    {
        public IDbConnection Connection { get; }
    }
}