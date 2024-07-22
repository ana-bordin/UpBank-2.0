namespace UPBank.Person.Domain.Contracts
{
    public interface IPersonRepository
    {
        Task<(Entities.Person okResult, string message)> CreatePerson(Entities.Person person);
        Task<(Entities.Person person, string message)> GetPersonByCpf(string cpf);
        Task<(Entities.Person personResult, string message)> PatchPerson(string cpf, Entities.Person person);
    }
}
