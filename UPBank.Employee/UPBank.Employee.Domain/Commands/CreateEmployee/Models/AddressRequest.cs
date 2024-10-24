namespace UPBank.Employee.Domain.Commands.CreateEmployee.Models
{
    public class AddressRequest
    {
        public string ZipCode { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }

        public static string GetOnlyNumbers(string zipcode) => zipcode.Replace(".", "").Replace("-", "");
    }
}