namespace UPBank.Address.Application.Models
{
    public class AddressOutputModel
    {
        public Guid Id { get; set; }
        public string ZipCode { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Neighborhood { get; set; }

        public AddressOutputModel(Guid id, string zipCode, string complement, string number, string street, string neighborhood, string state, string city)
        {
            Id = id;
            ZipCode = zipCode;
            Number = number;
            Complement = complement;
            Street = street;
            City = city;
            State = state;
            Neighborhood = neighborhood;
        }
    }

}