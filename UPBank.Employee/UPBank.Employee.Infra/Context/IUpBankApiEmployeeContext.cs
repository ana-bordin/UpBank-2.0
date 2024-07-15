using System.Data;

namespace UPBank.Employee.Infra.Context
{
    public interface IUpBankApiEmployeeContext
    {
        public IDbConnection Connection { get; }
    }
}
