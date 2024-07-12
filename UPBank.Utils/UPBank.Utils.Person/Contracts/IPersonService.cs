namespace UPBank.Utils.Person.Contracts
{
    public interface IPersonService
    {
        Task<bool> CreatePerson(UPBank.Person.Domain.Entities.Person person);
        Task<UPBank.Person.Domain.Entities.Person> GetPersonByCpf(string cpf);
        //Task<Person> PatchPerson(string cpf, PersonPatchDTO personPatchDTO);
        Task<bool> CheckIfExist(string cpf);
    }
}
