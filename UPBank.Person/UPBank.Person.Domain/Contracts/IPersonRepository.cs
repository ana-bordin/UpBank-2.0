namespace UPBank.Person.Domain.Contracts
{
    public interface IPersonRepository
    {
        Task<bool> CreatePerson(Entities.Person person);
        Task<Entities.Person> GetPersonByCpf(string cpf);
        Task<Entities.Person> PatchPerson(string cpf, Entities.Person person);
    }
}
