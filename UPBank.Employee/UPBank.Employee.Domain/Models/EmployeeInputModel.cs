using UPBank.Address.Application.Models;
using UPBank.Person.Application.Models;

namespace UPBank.Employee.Application.Models
{
    public class EmployeeInputModel : PersonInputModel
    {
        public bool Manager { get; set; }

    }
}
