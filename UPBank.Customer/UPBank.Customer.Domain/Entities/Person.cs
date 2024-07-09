using System.ComponentModel.DataAnnotations;

namespace UPBank.Customer.Domain.Entities
{
    public class Person
    {
        [Key]
        public string CPF { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //[NotMapped]
        //public CompleteAddress Address { get; set; }
        public Guid AddressId { get; set; }
    }
}
