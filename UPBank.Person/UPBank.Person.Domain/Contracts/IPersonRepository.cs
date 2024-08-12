namespace UPBank.Person.Domain.Contracts
{
    public interface IPersonRepository
    {
        Task<Entities.Person> CreatePerson(Entities.Person person);
        Task<Entities.Person> GetPersonByCpf(string cpf);
        Task<Entities.Person> PatchPerson(string cpf, Entities.Person person);
    }
}