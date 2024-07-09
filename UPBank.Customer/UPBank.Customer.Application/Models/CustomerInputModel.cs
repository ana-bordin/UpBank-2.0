namespace UPBank.Customer.Application.Models
{
    public class CustomerInputModel
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressInputModel Address { get; set; }
    }
}
