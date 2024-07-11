using UPBank.Customer.Application.Models;

namespace UPBank.Customer.Application.Contracts
{
    public interface ICustomerService
    {
        Task<Domain.Entities.Customer> CreateCustomer(string cpf);
        Task<IEnumerable<CustomerOutputModel>> GetAllCustomers();
        Task<Domain.Entities.Customer> GetCustomerByCpf(string cpf);
        Task<bool> DeleteCustomerByCpf(string cpf);
    }
}
