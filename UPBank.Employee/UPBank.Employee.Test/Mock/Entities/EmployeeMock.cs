using UPBank.Person.Test.Mocks.Entities;

namespace UPBank.Employee.Test.Mock.Entities
{
    public class EmployeeMock
    {
        public static EmployeeInputModel EmployeeInputModel => new EmployeeInputModel
        {
            CPF = "96649661007",
            Name = "Person Name",
            BirthDate = DateTime.Parse("2000/10/10"),
            Address = AddressMock.AddressInputModel,
            Email = "test@gmail.com",
            Gender = 'm',
            Phone = "16991999999",
            Salary = 30000
        };

        public static Domain.Entities.Employee Employee => new Domain.Entities.Employee
        {
            CPF = "96649661007",
            Name = "Person Name",
            BirthDate = DateTime.Parse("2000/10/10"),
            AddressId = new Guid(),
            Email = "test@gmail.com",
            Gender = 'm',
            Phone = "16991999999",
            Salary = 30000,
            Active = true,
            Manager = false,
            RecordNumber = new Guid()
        };

        public static EmployeePatchDTO EmployeePatchDTO => new EmployeePatchDTO
        {
            Salary = 40000
        };
    }
}
