namespace UPBank.Employee.Domain.Entities
{
    public class RequestAccount
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public Guid Request { get; set; }
    }
}
