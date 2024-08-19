using UPBank.Utils.CommonsFiles.Contracts.Repositories;

namespace UPBank.Customer.Domain.Contracts
{
    public interface ICustomerRepository : IRepository<Entities.Customer>
    {
        Task<(Entities.Customer customer, string message)> CustomerPatchRestriction(string cpf);
        Task<(IEnumerable<Entities.Customer> customers, string message)> GetAllCustomersWithRestriction();
        Task<(bool ok, string message)> AccountOpening(List<string> cpfs);
    }
}