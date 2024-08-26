using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Employee.Domain.Commands
{
    public class CreateEmployeeCommandResponse : CreatePersonCommandResponse   
    {
        public bool Manager { get; set; }
        public Guid RecordNumber { get; set; }
        public bool Active { get; set; }
    }
}