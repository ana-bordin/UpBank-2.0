using UPBank.Person.Application.Models;
using UPBank.Utils.Person.Models.DTOs;

namespace UPBank.Utils.Person.Contracts
{
    public interface IPersonService
    {
        Task<(bool ok, string message)> CreatePerson(PersonInputModel person);
        Task<UPBank.Person.Domain.Entities.Person> GetPersonByCpf(string cpf);
        Task<UPBank.Person.Domain.Entities.Person> PatchPerson(string cpf, PersonPatchDTO personPatchDTO);
        Task<bool> CheckIfExist(string cpf);
    }
}
