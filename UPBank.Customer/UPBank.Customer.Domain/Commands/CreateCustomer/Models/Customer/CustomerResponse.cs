using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Address;

namespace UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer
{
    public class CustomerResponse
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressResponse Address { get; set; }
        public bool Restriction { get; set; }
        public bool Active { get; set; }

        public static string CpfAddMask(string cpf)
        {
            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
        }
    }
}