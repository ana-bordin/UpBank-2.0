using UPBank.Account.Domain.Contracts;

namespace UPBank.Account.Infra.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Task<Domain.Entities.Account?> AddAsync(Domain.Entities.Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.Account>> GetAccountsByAgency()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.Account>> GetAccountsByOwner()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.Account>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Account?> GetOneAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Account?> UpdateAsync(Domain.Entities.Account entity)
        {
            throw new NotImplementedException();
        }
    }
}