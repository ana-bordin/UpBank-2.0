using UPBank.Utils.CommonsFiles.Contracts.Repositories;

namespace UPBank.Account.Domain.Contracts
{
    public interface IAccountRepository : IRepository<Entities.Account>
    {
        public Task<IEnumerable<Entities.Account>> GetAccountsByAgency();
        public Task<IEnumerable<Entities.Account>> GetAccountsByOwner();
    }
}