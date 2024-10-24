using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Address;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer;

namespace UPBank.Customer.Domain.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<CustomerResponse>
    {
        public string CPF { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public char Gender { get; set; } = ' ';
        public double Salary { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public AddressRequest Address { get; set; } = new AddressRequest();
    }
}