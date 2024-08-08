using System.ComponentModel.DataAnnotations;

namespace UPBank.Address.Domain.Entities
{
    public class CompleteAddress
    {
        [Key]
        public Guid Id { get; set; }
        public string ZipCode { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
    }
}
