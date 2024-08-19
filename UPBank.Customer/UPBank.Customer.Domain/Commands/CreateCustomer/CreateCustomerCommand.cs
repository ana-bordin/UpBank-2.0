using MediatR;
using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Customer.Domain.Commands.CreateCustomer
{
    public class CreateCustomerCommand : CreatePersonCommand, IRequest<CreateCustomerCommandResponse>
    {
    }
}
