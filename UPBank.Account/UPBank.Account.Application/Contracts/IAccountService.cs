using UPBank.Account.Application.Models;

namespace UPBank.Account.Application.Contracts
{
    public interface IAccountService
    {
        public Task<AccountOutputModel> GetAccount();
        public Task<AccountOutputModel> CreateAccount();
        public Task<AccountOutputModel> UpdateAccount();
        public Task<AccountOutputModel> DeleteAccount();
        public Task<IEnumerable<AccountOutputModel>> GetAccountsByAgency();
        public Task<IEnumerable<AccountOutputModel>> GetAccountsByOwner();
    }
}
