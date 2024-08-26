using System.Data;

namespace UPBank.Employee.Infra.Context
{
    public interface IUpBankApiEmployeeContext : IDisposable
    {
        public IDbConnection Connection { get; }
    }
}
