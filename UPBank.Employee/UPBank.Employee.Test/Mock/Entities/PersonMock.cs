namespace UPBank.Person.Test.Mocks.Entities
{
    public class PersonMock
    {
        public static PersonInputModel PersonInputModel => new PersonInputModel
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

        public static Domain.Entities.Person Person => new Domain.Entities.Person
        {
            CPF = "96649661007",
            Name = "Person Name",
            BirthDate = DateTime.Parse("2000/10/10"),
            AddressId = AddressMock.AddressOutputModel.Id,
            Email = "test@gmail.com",
            Gender = 'm',
            Phone = "16991999999",
            Salary = 30000

        };

        public static Domain.Entities.Person PersonGet => new Domain.Entities.Person
        {
            CPF = "96649661007",
            Name = "Person Name",
            BirthDate = DateTime.Parse("2000/10/10"),
            AddressId = AddressMock.AddressOutputModel.Id,
            Email = "test@gmail.com",
            Gender = 'm',
            Phone = "16991999999",
            Salary = 30000

        };

        public static Domain.Entities.Person PersonUpdate => new Domain.Entities.Person
        {
            CPF = "96649661007",
            Name = "Person Name",
            BirthDate = DateTime.Parse("2000/10/10"),
            AddressId = AddressMock.AddressOutputModel.Id,
            Email = "test@gmail.com",
            Gender = 'm',
            Phone = "16991999999",
            Salary = 30000

        };

        public static PersonPatchDTO PersonPatchDTO => new PersonPatchDTO
        {
            Name = "Person Name update",
            Email = ""
        };

        public static PersonOutputModel PersonOutputModel => new PersonOutputModel
        {
            CPF = "96649661007",
            Name = "Person Name",
            BirthDate = DateTime.Parse("2000/10/10"),
            Address = AddressMock.AddressOutputModel,
            Email = "test@gmail.com",
            Gender = 'm',
            Phone = "16991999999",
            Salary = 30000
        };

        public static PersonInputModel InvalidPersonCPF => new PersonInputModel
        {
            CPF = "222",
        };

        public static PersonInputModel InvalidPersonName => new PersonInputModel
        {
            CPF = PersonInputModel.CPF,
            Name = ""
        };

        public static PersonInputModel InvalidPersonEmail => new PersonInputModel
        {
            CPF = PersonInputModel.CPF,
            Name = PersonMock.PersonInputModel.Name,
            Email = "test"
        };

        public static PersonInputModel InvalidPersonPhone => new PersonInputModel
        {
            CPF = PersonInputModel.CPF,
            Name = PersonMock.PersonInputModel.Name,
            Email = PersonMock.PersonInputModel.Email,
            Phone = "123"
        };

        public static PersonInputModel InvalidPersonSalary => new PersonInputModel
        {
            CPF = PersonInputModel.CPF,
            Name = PersonMock.PersonInputModel.Name,
            Email = PersonMock.PersonInputModel.Email,
            Phone = PersonMock.PersonInputModel.Phone,
            Salary = -1
        };
    }
}
