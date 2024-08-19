using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Person.Domain.Commands.UpdatePerson;

namespace UPBank.Utils.Person.Contracts
{
    public interface IPersonService
    {
        Task<CreatePersonCommandResponse?> CreatePersonAsync(CreatePersonCommand createPersonCommand);
        Task<CreatePersonCommandResponse?> UpdatePersonAsync(string cpf, UpdatePersonCommand updatePersonCommand);
        Task<CreatePersonCommandResponse?> GetPersonByCPFAsync(string cpf);
    }
}