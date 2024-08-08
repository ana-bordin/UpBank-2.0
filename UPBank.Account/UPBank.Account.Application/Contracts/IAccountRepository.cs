namespace UPBank.Account.Domain.Contracts
{
    public interface IAccountRepository
    {
        public Task<Entities.Account> GetAccount();
        public Task<Entities.Account> CreateAccount();
        public Task<Entities.Account> UpdateAccount();
        public Task<Entities.Account> DeleteAccount();
        public Task<IEnumerable<Entities.Account>> GetAccountsByAgency();
        public Task<IEnumerable<Entities.Account>> GetAccountsByOwner();
    }

}
