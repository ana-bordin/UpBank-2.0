using UPBank.Address.Domain.Commands.CreateAddress;

namespace UPBank.Person.Domain.Commands.CreatePerson
{
    public class CreatePersonCommandResponse
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CreateAddressCommandResponse Address { get; set; }

        public static string CpfAddMask(string cpf)
        {
            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
        }
    }
}