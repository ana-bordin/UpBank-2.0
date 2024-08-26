using System.Data;

namespace UPBank.Agency.Infra.Context
{
    public interface IUpBankApiAgencyContext : IDisposable
    {
        public IDbConnection Connection { get; }
    }
}