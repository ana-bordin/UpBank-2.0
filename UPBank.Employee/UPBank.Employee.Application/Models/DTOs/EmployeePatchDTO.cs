using UPBank.Address.Application.Models;
using UPBank.Utils.Person.Models.DTOs;

namespace UPBank.Employee.Application.Models.DTOs
{
    public class EmployeePatchDTO : PersonPatchDTO
    {
        public bool Manager { get; set; }
    }
}
