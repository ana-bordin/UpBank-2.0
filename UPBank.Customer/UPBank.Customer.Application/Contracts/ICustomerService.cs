using UPBank.Customer.Application.Models;

namespace UPBank.Customer.Application.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerOutputModel> CreateCustomer(string cpf);
        Task<IEnumerable<CustomerOutputModel>> GetAllCustomers();
        Task<CustomerOutputModel> GetCustomerByCpf(string cpf);
        Task<bool> DeleteCustomerByCpf(string cpf);
        Task<CustomerOutputModel> CreateCustomerOutputModel(Domain.Entities.Customer customer);
        Task<bool> CheckIfExists(string cpf);







        Task<CustomerOutputModel> CustomerRestriction(string cpf);
        Task<IEnumerable<CustomerOutputModel>> GetCustomersWithRestriction();

        Task <bool> AccountOpening (List<string> cpfs);

    }
}
