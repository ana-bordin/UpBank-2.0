using System.Data;

namespace UPBank.Account.Infra.Context
{
    public interface IUpBankApiAccountContext
    {
        public IDbConnection ConnectionAccount { get; }
    }
}

