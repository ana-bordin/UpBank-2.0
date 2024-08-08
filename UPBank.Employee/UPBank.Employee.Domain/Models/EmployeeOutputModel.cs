using UPBank.Address.Application.Models;
using UPBank.Person.Application.Models;

namespace UPBank.Employee.Application.Models
{
    public class EmployeeOutputModel : PersonOutputModel
    {
        public bool Manager { get; set; }
    }
}
