using UPBank.Customer.Application.Models;

namespace UPBank.Utils.Person.Models.DTOs
{
    public class PersonPatchDTO
    {
        public string Name { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressInputModel Address { get; set; }
    }
}
