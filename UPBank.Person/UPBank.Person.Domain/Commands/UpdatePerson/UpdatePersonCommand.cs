using MediatR;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Commands.UpdateAddress;
using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Person.Domain.Commands.UpdatePerson
{
    public class UpdatePersonCommand :  IRequest<CreatePersonCommandResponse>
    {
        public string CPF { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public char Gender { get; set; } = ' ';
        public double Salary { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public UpdateAddressCommand Address { get; set; } = new UpdateAddressCommand();
    }
}