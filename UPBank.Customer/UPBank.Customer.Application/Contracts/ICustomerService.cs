using UPBank.Customer.Application.Models;
using UPBank.Utils.Person.Models.DTOs;

namespace UPBank.Customer.Application.Contracts
{
    public interface ICustomerService
    {
        Task<(CustomerOutputModel customerOutputModel, string message)> CreateCustomer(string cpf);
        Task<(IEnumerable<CustomerOutputModel> customers, string message)> GetAllCustomers();
        Task<(CustomerOutputModel customerOutputModel, string message)> GetCustomerByCpf(string cpf);
        Task<(bool ok, string message)> DeleteCustomerByCpf(string cpf);
        Task<CustomerOutputModel> CreateCustomerOutputModel(Domain.Entities.Customer customer);
        Task<(CustomerOutputModel customerOutputodel, string message)> CustomerPatchRestriction(string cpf);
        Task<(IEnumerable<CustomerOutputModel> customers, string message)>GetAllCustomersWithRestriction();
        Task <(bool ok, string message)> AccountOpening (List<string> cpfs);
        Task <(CustomerOutputModel customerOutputModel, string message)> UpdateCustomer(string cpf, PersonPatchDTO personPatchDTO);
    }
}
