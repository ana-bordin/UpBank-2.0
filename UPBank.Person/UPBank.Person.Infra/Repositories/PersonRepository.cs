using Dapper;
using UPBank.Person.Domain.Contracts;
using UPBank.Person.Infra.Context;
using UPBank.Utils.CommonsFiles.Contracts;

namespace UPBank.Person.Infra.Repositories
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

        public async Task<Domain.Entities.Person> CreatePerson(Domain.Entities.Person person)
        {
            try
            {
                var rows = await _context.Connection.ExecuteAsync("INSERT INTO dbo.Person (Name, BirthDate, CPF, Email, Phone, Gender, Salary, AddressId) VALUES (@Name, @BirthDate, @CPF, @Email, @Phone, @Gender, @Salary, @AddressId)", new { Name = person.Name, BirthDate = person.BirthDate, CPF = person.CPF, Email = person.Email, Phone = person.Phone, Gender = person.Gender, Salary = person.Salary, AddressId = person.AddressId });

                return await GetPersonByCpf(person.CPF);
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao adicionar pessoa:" + e.Message);
                return null;
            }
        }

        public async Task<Domain.Entities.Person> GetPersonByCpf(string cpf)
        {
            try
            {
                var person = await _context.Connection.QueryFirstOrDefaultAsync<Domain.Entities.Person>("SELECT * FROM dbo.Person WHERE CPF = @CPF", new { CPF = cpf });
                return person;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao buscar pessoa:" + e.Message);
                return null;
            }
        }
        public async Task<Domain.Entities.Person> PatchPerson(string cpf, Domain.Entities.Person person)
        {
            try
            {
                var rows = await _context.Connection.ExecuteAsync("UPDATE dbo.Person SET Name = @Name, Email = @Email, Phone = @Phone, Gender = @Gender, Salary = @Salary WHERE CPF = @CPF", new { Name = person.Name, Email = person.Email, Phone = person.Phone, Gender = person.Gender, Salary = person.Salary, CPF = cpf });

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

        _context.Connection.Dispose();
        
    }
}