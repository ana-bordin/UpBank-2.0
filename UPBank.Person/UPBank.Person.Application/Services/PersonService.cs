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

        public Task<bool> CreatePerson(Domain.Entities.Person person)
        {
            person.Validate(person);
            return _personRepository.CreatePerson(person);
        }

        public Task<bool> CheckIfExist(string cpf)
        {
            return _personRepository.CheckIfExist(cpf);
        }

        public Task<Domain.Entities.Person> GetPersonByCpf(string cpf)
        {
            return _personRepository.GetPersonByCpf(cpf);
        }

        public Task<Domain.Entities.Person> PatchPerson(string cpf, Models.DTOs.PersonPatchDTO personPatchDTO)
        {
            var person = _personRepository.GetPersonByCpf(cpf);
            if (personPatchDTO.Name != person.Result.Name && personPatchDTO.Name != "")
                person.Result.Name = personPatchDTO.Name;

            if (personPatchDTO.Gender != person.Result.Gender && personPatchDTO.Gender != null)
                person.Result.Gender = personPatchDTO.Gender;

            if (personPatchDTO.Salary != person.Result.Salary && personPatchDTO.Salary != 0)
                person.Result.Salary = personPatchDTO.Salary;

            if (personPatchDTO.Email != person.Result.Email && personPatchDTO.Email != "")
                person.Result.Email = personPatchDTO.Email;

            if (personPatchDTO.Phone != person.Result.Phone && personPatchDTO.Phone != "")
                person.Result.Phone = personPatchDTO.Phone;

            return _personRepository.PatchPerson(cpf, person.Result);
        }
    }
}
