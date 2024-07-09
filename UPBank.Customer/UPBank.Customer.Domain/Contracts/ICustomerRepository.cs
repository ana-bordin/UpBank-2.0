namespace UPBank.Customer.Domain.Contracts
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomer(string cpf);
        Task<Entities.Customer> GetCustomerByCpf(string cpf);
        Task<bool> DeleteCustomerByCpf(string cpf);
        Task<IEnumerable<Entities.Customer>> GetAllCustomers();
    }
}
