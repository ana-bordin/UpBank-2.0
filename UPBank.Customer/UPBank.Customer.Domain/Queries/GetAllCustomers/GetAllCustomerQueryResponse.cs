using UPBank.Customer.Domain.Commands.CreateCustomer;

namespace UPBank.Customer.Domain.Queries.GetAllCustomers
{
    public class GetAllCustomerQueryResponse
    {
        public IEnumerable<CreateCustomerCommandResponse> Customers { get; set; } = new List<CreateCustomerCommandResponse>();
    }
}