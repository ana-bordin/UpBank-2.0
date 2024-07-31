using UPBank.Account.Application.Contracts;
using UPBank.Account.Application.Models;

namespace UPBank.Account.Application.Services
{
    public class AccountService : IAccountService
    {
        public Task<AccountOutputModel> CreateAccount()
        {
            throw new NotImplementedException();
        }

        public Task<AccountOutputModel> DeleteAccount()
        {
            throw new NotImplementedException();
        }

        public Task<AccountOutputModel> GetAccount()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountOutputModel>> GetAccountsByAgency()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountOutputModel>> GetAccountsByOwner()
        {
            throw new NotImplementedException();
        }

        public Task<AccountOutputModel> UpdateAccount()
        {
            throw new NotImplementedException();
        }
    }
}
