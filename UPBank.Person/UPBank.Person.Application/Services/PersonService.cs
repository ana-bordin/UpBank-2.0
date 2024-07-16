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

        public async Task<Domain.Entities.Person> CreatePerson(Domain.Entities.Person person)
        {
            person.CPF = person.CpfRemoveMask(person.CPF);
            person.Validate(person);
            if (await _personRepository.CreatePerson(person))
                return await GetPersonByCpf(person.CPF);
            return null;
        }

        public async Task<bool> CheckIfExist(string cpf)
        {
            if (await _personRepository.GetPersonByCpf(cpf) != null)
                return true;
            return false;
        }

        public async Task<Domain.Entities.Person> GetPersonByCpf(string cpf)
        {
            var person = await _personRepository.GetPersonByCpf(cpf);
            person.CPF = person.CpfAddMask(person.CPF);
            return person;
        }

        public async Task<Domain.Entities.Person> PatchPerson(string cpf, Models.DTOs.PersonPatchDTO personPatchDTO)
        {
            var person = await _personRepository.GetPersonByCpf(cpf);
            if (personPatchDTO.Name != person.Name && personPatchDTO.Name != "")
                person.Name = personPatchDTO.Name;

            if (personPatchDTO.Gender != person.Gender && personPatchDTO.Gender.ToString() != null)
                person.Gender = personPatchDTO.Gender;

            if (personPatchDTO.Salary != person.Salary && personPatchDTO.Salary != 0)
                person.Salary = personPatchDTO.Salary;

            if (personPatchDTO.Email != person.Email && personPatchDTO.Email != "")
                person.Email = personPatchDTO.Email;

            if (personPatchDTO.Phone != person.Phone && personPatchDTO.Phone != "")
                person.Phone = personPatchDTO.Phone;

            return await _personRepository.PatchPerson(cpf, person);
        }
    }
}
