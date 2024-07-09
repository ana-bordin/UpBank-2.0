using UPBank.Customer.Application.Models.DTOs;
using UPBank.Customer.Domain.Entities;

namespace UPBank.Customer.Application.Contracts
{
    public interface IPersonService
    {
        Task<bool> CreatePerson(Person person);
        Task<Person> GetPersonByCpf(string cpf);
        Task<bool> PatchPerson(string cpf, PersonPatchDTO personPatchDTO);
        Task<bool> CheckIfExist(string cpf);
    }
}
