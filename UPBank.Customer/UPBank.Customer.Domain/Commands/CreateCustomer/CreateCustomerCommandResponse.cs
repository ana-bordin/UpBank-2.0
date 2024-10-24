using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer;

namespace UPBank.Customer.Domain.Commands.CreateCustomer
{
    public class CreateCustomerCommandResponse
    {
        public List<CustomerResponse> Customers { get; set; }
    }
}