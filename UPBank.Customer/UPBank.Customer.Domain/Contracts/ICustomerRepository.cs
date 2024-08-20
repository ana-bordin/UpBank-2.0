using UPBank.Utils.CommonsFiles.Contracts.Repositories;

namespace UPBank.Customer.Domain.Contracts
{
    namespace UPBank.Customer.Domain.Contracts
    {
        public interface ICustomerRepository : IRepository<Entities.Customer>
        {
            Task<Entities.Customer> CustomerPatchRestriction(string cpf);
            Task<IEnumerable<Entities.Customer>> GetAllCustomersWithRestriction();
            Task<bool> AccountOpening(List<string> cpfs);
        }
    }
}