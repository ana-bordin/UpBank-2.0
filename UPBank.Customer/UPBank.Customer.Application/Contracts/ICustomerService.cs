using UPBank.Customer.Application.Models;

namespace UPBank.Customer.Application.Contracts
{
    public interface ICustomerService
    {
        Task<(CustomerOutputModel customerOutputModel, string message)> CreateCustomer(string cpf);
        Task<IEnumerable<CustomerOutputModel>> GetAllCustomers();
        Task<(CustomerOutputModel customerOutputModel, string message)> GetCustomerByCpf(string cpf);
        Task<(bool ok, string message)> DeleteCustomerByCpf(string cpf);
        Task<CustomerOutputModel> CreateCustomerOutputModel(Domain.Entities.Customer customer);
        Task<string> CheckIfExists(string cpf);
        Task<CustomerOutputModel> CustomerRestriction(string cpf);
        Task<IEnumerable<CustomerOutputModel>> GetCustomersWithRestriction();



        Task <bool> AccountOpening (List<string> cpfs);

    }
}
