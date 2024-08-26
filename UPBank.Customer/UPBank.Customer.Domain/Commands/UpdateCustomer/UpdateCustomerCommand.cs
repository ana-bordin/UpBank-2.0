using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer;
using UPBank.Person.Domain.Commands.UpdatePerson;

namespace UPBank.Customer.Domain.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : UpdatePersonCommand, IRequest<CreateCustomerCommandResponse>
    {
    }
}