using System.Data;

namespace UPBank.Address.Infra.Context
{
    public interface IUpBankApiAddressContext : IDisposable
    {
        public IDbConnection Connection { get; }
    }
}