using UPBank.Person.Application.Models;
using UPBank.Utils.Person.Models.DTOs;

namespace UPBank.Utils.Person.Contracts
{
    public interface IPersonService
    {
        Task<(bool ok, string message)> CreatePerson(PersonInputModel person);
        Task<(PersonOutputModel person, string message)> GetPersonByCpf(string cpf);
        Task<(PersonOutputModel person, string message)> PatchPerson(string cpf, PersonPatchDTO personPatchDTO);
        Task<bool> CheckIfExist(string cpf);
    }
}
