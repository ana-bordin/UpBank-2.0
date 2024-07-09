using UPBank.Customer.Domain.Entities;

namespace UPBank.Customer.Domain.Contracts
{
    public interface IPersonRepository
    {
        Task<bool> CreatePerson(Person person);
        Task<Person> GetPersonByCpf(string cpf);
        //Task<bool> PatchPerson(string cpf, Models.DTOs.PersonPatchDTO personPatchDTO);
        Task<bool> CheckIfExist(string cpf);
    }
}
