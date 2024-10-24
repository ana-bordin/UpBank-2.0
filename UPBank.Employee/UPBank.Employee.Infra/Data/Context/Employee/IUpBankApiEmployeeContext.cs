using System.Data;

namespace UPBank.Employee.Infra.Data.Context.Employee
{
    public interface IUpBankApiEmployeeContext : IDisposable
    {
        public IDbConnection Connection { get; }
    }
}
