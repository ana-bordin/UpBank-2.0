using UPBank.Person.Application.Models;

namespace UPBank.Customer.Application.Models
{
    public class CustomerOutputModel : PersonOutputModel
    {
        public bool Restriction { get; set; }
    }
}
