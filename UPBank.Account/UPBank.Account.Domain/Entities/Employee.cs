namespace UPBank.Employee.Domain.Entities
{
    public class Employee : Person.Domain.Entities.Person
    {
        public bool Manager { get; set; }
        public Guid RecordNumber { get; set; }
        public bool Active { get; set; }
    }
}