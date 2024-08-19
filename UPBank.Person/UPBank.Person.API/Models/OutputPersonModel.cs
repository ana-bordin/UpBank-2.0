using UPBank.Address.API.Models;

namespace UPBank.Person.API.Models
{
    public class OutputPersonModel
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public OutputAddressModel Address { get; set; }
    }
}
