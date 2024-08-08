namespace UPBank.Customer.Domain.Contracts
{
    public interface ICustomerRepository
    {
        Task<(Entities.Customer customer, string message)> CreateCustomer(string cpf);
        Task<(Entities.Customer customer, string message)> GetCustomerByCpf(string cpf);
        Task<(bool ok, string message)> DeleteCustomerByCpf(string cpf);
        Task<(IEnumerable<Entities.Customer> customers, string message)> GetAllCustomers();
        Task<(Entities.Customer customer, string message)> CustomerPatchRestriction(string cpf);
        Task<(IEnumerable<Entities.Customer> customers, string message)> GetAllCustomersWithRestriction();
        Task<(bool ok, string message)> AccountOpening(List<string> cpfs);
    }
}
