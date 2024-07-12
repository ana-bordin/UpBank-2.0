using UPBank.Person.Application.Models.DTOs;

namespace UPBank.Person.Application.Contracts
{
    public interface IPersonService
    {
        Task<bool> CreatePerson(Domain.Entities.Person person);
        Task<Domain.Entities.Person> GetPersonByCpf(string cpf);
        Task<Domain.Entities.Person> PatchPerson(string cpf, PersonPatchDTO personPatchDTO);
        Task<bool> CheckIfExist(string cpf);
    }
}
