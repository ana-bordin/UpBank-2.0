using UPBank.Person.Application.Models;
using UPBank.Utils.Person.Models.DTOs;

namespace UPBank.Utils.Person.Contracts
{
    public interface IPersonService
    {
        Task<(PersonOutputModel okResult, string message)> CreatePerson(PersonInputModel person);
        Task<(PersonOutputModel person, string message)> GetPersonByCpf(string cpf);
        Task<(PersonOutputModel person, string message)> PatchPerson(string cpf, PersonPatchDTO personPatchDTO);
        Task<bool> CheckIfExist(string cpf);
        //Task<PersonInputModel> CreatePersonInputModel<T>(T inputModel) where T : class;
    }
}
