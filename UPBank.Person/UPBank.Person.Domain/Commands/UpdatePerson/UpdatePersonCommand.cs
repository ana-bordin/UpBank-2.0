using MediatR;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Person.Domain.Commands.CreatePerson.Models.Address;

namespace UPBank.Person.Domain.Commands.UpdatePerson
{
    public class UpdatePersonCommand : IRequest<CreatePersonCommandResponse>
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