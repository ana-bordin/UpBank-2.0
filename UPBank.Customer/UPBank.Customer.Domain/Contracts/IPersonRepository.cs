using UPBank.Customer.Domain.Entities;

namespace UPBank.Customer.Domain.Contracts
{
    public interface IPersonRepository
    {
        Task<bool> CreatePerson(Person person);
        Task<Person> GetPersonByCpf(string cpf);
        Task<Person> PatchPerson(string cpf, PersonPatchDTO personPatchDTO);
        Task<bool> CheckIfExist(string cpf);
    }
}
