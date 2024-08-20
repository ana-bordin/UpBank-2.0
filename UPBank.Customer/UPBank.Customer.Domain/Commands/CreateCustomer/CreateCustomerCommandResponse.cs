using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Customer.Domain.Commands.CreateCustomer
{
    public class CreateCustomerCommandResponse : CreatePersonCommand
    {
        public bool Restriction { get; set; }
        public bool Active { get; set; }
    }
}