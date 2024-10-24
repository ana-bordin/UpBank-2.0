using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Employee.Domain.Commands.CreateEmployee.Models;

namespace UPBank.Employee.Domain.Commands.CreateEmployee
{
    public class CreateEmployeeCommandResponse
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Manager { get; set; }
        public Guid RecordNumber { get; set; }
        public bool Active { get; set; }
        public AddressResponse Address { get; set; }

        public static string CpfAddMask(string cpf) => $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
    }
}