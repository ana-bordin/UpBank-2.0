using MediatR;
using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Customer.Domain.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponseList>
    { 
        public List<CreatePersonCommand> Customers { get; set; } = new List<CreatePersonCommand>();

    }   
}