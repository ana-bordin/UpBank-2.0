using UPBank.Account.Domain.Contracts;

namespace UPBank.Account.Infra.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Task<Domain.Entities.Account> CreateAccount()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Account> DeleteAccount()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Account> GetAccount()
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

        public Task<Domain.Entities.Account> UpdateAccount()
        {
            throw new NotImplementedException();
        }
    }
}
