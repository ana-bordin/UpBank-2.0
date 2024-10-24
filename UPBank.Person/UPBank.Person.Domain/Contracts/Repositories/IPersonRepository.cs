namespace UPBank.Person.Domain.Contracts.Repositories
{
    public interface IPersonRepository
    {
        Task<Entities.Person.Person> CreatePerson(Entities.Person.Person person);
        Task<Entities.Person.Person> GetPersonByCpf(string cpf);
        Task<Entities.Person.Person> PatchPerson(string cpf, Entities.Person.Person person);
    }
}