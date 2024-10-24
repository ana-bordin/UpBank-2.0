using Dapper;
using UPBank.Person.Domain.Contracts.Repositories;
using UPBank.Person.Infra.Data.Context;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Person.Infra.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IUpBankApiPersonContext _context;
        private readonly IDomainNotificationService _domainNotificationService;

        public PersonRepository(IUpBankApiPersonContext context, IDomainNotificationService domainNotificationService)
        {
            _domainNotificationService = domainNotificationService;
            _context = context;
        }

        public async Task<Domain.Entities.Person.Person> CreatePerson(Domain.Entities.Person.Person person)
        {
            try
            {
                var rows = await _context.Connection.ExecuteAsync("INSERT INTO dbo.Person (Name, CPF, BirthDate, Email, Phone, Gender, Salary, AddressId) VALUES (@Name, @CPF, @BirthDate, @Email, @Phone, @Gender, @Salary, @AddressId)", new { person.Name, person.CPF, person.BirthDate, person.Email, person.Phone, person.Gender, person.Salary, person.AddressId });

                return await GetPersonByCpf(person.CPF);
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao adicionar pessoa:" + e.Message);
                return null;
            }
        }

        public async Task<Domain.Entities.Person.Person> GetPersonByCpf(string cpf)
        {
            try
            {
                var person = await _context.Connection.QueryFirstOrDefaultAsync<Domain.Entities.Person.Person>("SELECT * FROM dbo.Person WHERE CPF = @CPF", new { CPF = cpf });
                return person;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao buscar pessoa:" + e.Message);
                return null;
            }
        }
        public async Task<Domain.Entities.Person.Person> PatchPerson(string cpf, Domain.Entities.Person.Person person)
        {
            try
            {
                var rows = await _context.Connection.ExecuteAsync("UPDATE dbo.Person SET Name = @Name, Email = @Email, Phone = @Phone, Gender = @Gender, Salary = @Salary WHERE CPF = @CPF", new { person.Name, person.Email, person.Phone, person.Gender, person.Salary, CPF = cpf });

                if (rows > 0)
                    return await GetPersonByCpf(person.CPF);
                else
                    return null;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao atualizar pessoa:" + e.Message);
                return null;
            }
        }
    }
}