namespace UPBank.Customer.Application.Models
{
    public class CustomerOutputModel
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressOutputModel Address { get; set; }
        public bool Restriction { get; set; }
    }
}
