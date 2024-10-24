using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer;

namespace UPBank.Customer.Domain.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponse>
    {
        public List<CustomerRequest> Customers { get; set; } = new List<CustomerRequest>();

    }
}