using UPBank.Person.Application.Models;
using UPBank.Person.Application.Models.DTOs;

namespace UPBank.Person.Application.Contracts
{
    public interface IPersonService
    {
        Task<(bool okResult, string message)> CreatePerson(Domain.Entities.Person personResult);
        Task<(Domain.Entities.Person person, string message)> GetPersonByCpf(string cpf);
        Task<(Domain.Entities.Person person, string message)> PatchPerson(string cpf, PersonPatchDTO personPatchDTO);
    }
}
