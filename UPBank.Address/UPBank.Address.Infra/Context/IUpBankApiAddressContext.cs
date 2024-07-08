using System.Data;

namespace UPBank.Address.Infra.Context
{
    public interface IUpBankApiAddressContext
    {
        public IDbConnection Connection { get; }
    }
}
