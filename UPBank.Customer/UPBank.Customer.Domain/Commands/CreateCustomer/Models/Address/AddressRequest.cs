namespace UPBank.Customer.Domain.Commands.CreateCustomer.Models.Address
{
    public class AddressRequest
    {
        public string ZipCode { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Complement { get; set; }

        public static string GetOnlyNumbers(string zipcode)
        {
            var stringToBeConverted = zipcode.Replace("-", "").Replace(".", "").Replace(" ", "");
            return stringToBeConverted;
        }
    }
}
