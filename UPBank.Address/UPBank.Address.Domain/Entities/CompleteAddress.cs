using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPBank.Address.Domain.Entities
{
    public class CompleteAddress
    {
        [Key]
        public Guid Id { get; set; }
        public string ZipCode { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }

        [NotMapped]
        public Address Address { get; set; }
    }
}