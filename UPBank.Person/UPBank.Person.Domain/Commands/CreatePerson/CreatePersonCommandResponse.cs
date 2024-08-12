using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Utils.CommonsFiles.DTOs;

namespace UPBank.Person.Domain.Commands.CreatePerson
{
    public class CreatePersonCommandResponse : ResponseDTO
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CreateAddressCommandResponse Address { get; set; }
    }
}
