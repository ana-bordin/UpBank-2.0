using UPBank.Person.Application.Contracts;
using UPBank.Person.Application.Models;
using UPBank.Person.Domain.Contracts;
using UPBank.Utils.Address.Contracts;

namespace UPBank.Person.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAddressService _addressService;

        public PersonService(IPersonRepository personRepository, IAddressService addressService)
        {
            _personRepository = personRepository;
            _addressService = addressService;
        }

        public async Task<(PersonOutputModel okResult, string message)> CreatePerson(PersonInputModel personInputModel)
        {
            personInputModel.CPF = Domain.Entities.Person.CpfRemoveMask(personInputModel.CPF);
            var personResult = await _personRepository.GetPersonByCpf(personInputModel.CPF);

            if (personResult.person != null)
                return (CreatePersonOutputModel(personResult.person).Result, personResult.message);

            var add = await _addressService.CreateAddress(personInputModel.Address);

            var entityPerson = new Domain.Entities.Person
            {
                CPF = personInputModel.CPF,
                Name = personInputModel.Name,
                BirthDate = personInputModel.BirthDate,
                AddressId = add.addressOutputModel.Id,
                Email = personInputModel.Email,
                Gender = personInputModel.Gender,
                Phone = personInputModel.Phone,
                Salary = personInputModel.Salary
            };

            var validateCPF = entityPerson.Validate(entityPerson);

            if (validateCPF != string.Empty)
                return (null, validateCPF);

            personResult = await _personRepository.CreatePerson(entityPerson);
            if (personResult.person == null)
                return (null, personResult.message);

            return (CreatePersonOutputModel(personResult.person).Result, personResult.message);
        }

        public async Task<(PersonOutputModel person, string message)> GetPersonByCpf(string cpf)
        {
            cpf = Domain.Entities.Person.CpfRemoveMask(cpf);
            
            var person = await _personRepository.GetPersonByCpf(cpf);
            
            if (person.person == null)
                return (null, person.message);

            var personResult = CreatePersonOutputModel(person.person).Result;

            return (personResult, null);
        }

        public async Task<(Domain.Entities.Person person, string message)> PatchPerson(string cpf, Models.DTOs.PersonPatchDTO personPatchDTO)
        {
            var personResult = await _personRepository.GetPersonByCpf(cpf);

            if (personPatchDTO.Name != personResult.person.Name && personPatchDTO.Name != "")
                personResult.person.Name = personPatchDTO.Name;

            if (personPatchDTO.Gender != personResult.person.Gender && personPatchDTO.Gender.ToString() != null)
                personResult.person.Gender = personPatchDTO.Gender;

            if (personPatchDTO.Salary != personResult.person.Salary && personPatchDTO.Salary != 0)
                personResult.person.Salary = personPatchDTO.Salary;

            if (personPatchDTO.Email != personResult.person.Email && personPatchDTO.Email != "")
                personResult.person.Email = personPatchDTO.Email;

            if (personPatchDTO.Phone != personResult.person.Phone && personPatchDTO.Phone != "")
                personResult.person.Phone = personPatchDTO.Phone;

            if (personPatchDTO.Address.Number != "" || personPatchDTO.Address.Complement != "" || personPatchDTO.Address.ZipCode != "")
                await _addressService.UpdateAddress(personResult.person.AddressId, personPatchDTO.Address);

            return await _personRepository.PatchPerson(cpf, personResult.person);
        }

        public async Task<PersonOutputModel> CreatePersonOutputModel(Domain.Entities.Person person)
        {
            var address = await _addressService.GetCompleteAddressById(person.AddressId);

            var personOutputModel = new PersonOutputModel
            {
                CPF = person.CpfAddMask(person.CPF),
                Email = person.Email,
                Name = person.Name,
                Address = address.addressOutputModel,
                BirthDate = person.BirthDate,
                Gender = person.Gender,
                Phone = person.Phone,
                Salary = person.Salary
            };

            return personOutputModel;
        }

    }
}
