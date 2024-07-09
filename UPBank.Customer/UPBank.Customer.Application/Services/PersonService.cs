using UPBank.Customer.Application.Contracts;
using UPBank.Customer.Application.Models;
using UPBank.Customer.Domain.Contracts;
using UPBank.Customer.Domain.Entities;

namespace UPBank.Customer.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Task<bool> CreatePerson(Person person)
        {
            return _personRepository.CreatePerson(person);
        }

        public Task<bool> CheckIfExist(string cpf)
        {
            return _personRepository.CheckIfExist(cpf);
        }

        public Task<Person> GetPersonByCpf(string cpf)
        {
            return _personRepository.GetPersonByCpf(cpf);
        }

        public Task<bool> PatchPerson(string cpf, Models.DTOs.PersonPatchDTO personPatchDTO)
        {
            throw new NotImplementedException();
        }
    }
}
