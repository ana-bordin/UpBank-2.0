using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer;

namespace UPBank.Customer.Domain.Queries.GetAllCustomers
{
    public class GetAllCustomerQueryResponse
    {
        public IEnumerable<CustomerResponse> Customers { get; set; } = new List<CustomerResponse>();
    }
}