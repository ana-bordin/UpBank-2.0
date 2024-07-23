namespace UPBank.Customer.Domain.Contracts
{
    public interface ICustomerRepository
    {
        Task<(Entities.Customer customer, string message)> CreateCustomer(string cpf);
        Task<(Entities.Customer customer, string message)> GetCustomerByCpf(string cpf);
        Task<(bool ok, string message)> DeleteCustomerByCpf(string cpf);
        Task<IEnumerable<Entities.Customer>> GetAllCustomers();
        Task<Entities.Customer> CustomerRestriction(string cpf);
        Task<IEnumerable<Entities.Customer>> GetCustomersWithRestriction();


        Task<(bool ok, string message)> AccountOpening(List<string> cpfs);
    }
}
