namespace UPBank.Person.Domain.Entities.Address
{
    public class CompleteAddress
    {
        public Guid Id { get; set; }
        public Address Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Neighborhood { get; set; }
    }
}