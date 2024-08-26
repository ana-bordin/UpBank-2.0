using System.Data;

namespace UPBank.Account.Infra.Context
{
    public interface IUpBankApiAccountContext : IDisposable
    {
        public IDbConnection ConnectionAccount { get; }
    }
}

