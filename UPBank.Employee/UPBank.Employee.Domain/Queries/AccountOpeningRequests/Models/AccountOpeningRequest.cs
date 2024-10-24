using UPBank.Employee.Domain.Models;

namespace UPBank.Employee.Domain.Queries.AccountOpeningRequests.Models
{
    public class AccountOpeningRequest
    {
        public Guid Request { get; set; }
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AddressResponse Address { get; set; }
    }
}