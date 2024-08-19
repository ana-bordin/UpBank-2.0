using UPBank.Address.API.Models;

namespace UPBank.Person.API.Models
{
    public class InputPatchPersonModel
    {
        public string Name { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public InputAddressModel Address { get; set; }
    }
}