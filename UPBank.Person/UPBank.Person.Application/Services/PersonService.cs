using UPBank.Person.Application.Contracts;
using UPBank.Person.Domain.Contracts;

namespace UPBank.Person.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<(bool okResult, string message)> CreatePerson(Domain.Entities.Person person)
        {
            person.CPF = person.CpfRemoveMask(person.CPF);
            var validateCPF = person.Validate(person);

            if (validateCPF != string.Empty)
                return (false, validateCPF);

            return await _personRepository.CreatePerson(person);
        }

        public async Task<(Domain.Entities.Person person, string message)> GetPersonByCpf(string cpf)
        {
            var person = await _personRepository.GetPersonByCpf(cpf);
            if (person.person != null)
                person.person.CPF = person.person.CpfAddMask(person.person.CPF);

            return person;
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

            return await _personRepository.PatchPerson(cpf, personResult.person);
        }
    }
}
