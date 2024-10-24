using Dapper;
using UPBank.Customer.Domain.Contracts.Repositories;
using UPBank.Customer.Domain.Entities;
using UPBank.Customer.Infra.Data.Context.Person;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Customer.Infra.Data.Repostories
{
    internal class PersonRepository : IRepository<Person>
    {
        private readonly IUpBankApiPersonContext _context;
        private readonly IDomainNotificationService _domainNotificationService;

        public PersonRepository(IUpBankApiPersonContext context, IDomainNotificationService domainNotificationService)
        {
            _domainNotificationService = domainNotificationService;
            _context = context;
        }

        public async Task<Person> AddAsync(Person person)
        {
            try
            {
                var rows = await _context.Connection.ExecuteAsync("INSERT INTO dbo.Person (Name, CPF, BirthDate, Email, Phone, Gender, Salary, AddressId) VALUES (@Name, @CPF, @BirthDate, @Email, @Phone, @Gender, @Salary, @AddressId)", new { person.Name, person.CPF, person.BirthDate, person.Email, person.Phone, person.Gender, person.Salary, person.AddressId });

                return await GetOneAsync(person.CPF);
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao adicionar pessoa:" + e.Message);
                return null;
            }
        }
        public async Task<Person> GetOneAsync(string cpf)
        {
            try
            {
                var person = await _context.Connection.QueryFirstOrDefaultAsync<Person>("SELECT * FROM dbo.Person WHERE CPF = @CPF", new { CPF = cpf });
                return person;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao buscar pessoa:" + e.Message);
                return null;
            }
        }
        public async Task<Person> UpdateAsync(Person person)
        {
            try
            {
                var rows = await _context.Connection.ExecuteAsync("UPDATE dbo.Person SET Name = @Name, Email = @Email, Phone = @Phone, Gender = @Gender, Salary = @Salary WHERE CPF = @CPF", new { person.Name, person.Email, person.Phone, person.Gender, person.Salary, CPF = person.CPF });

                if (rows > 0)
                    return await GetOneAsync(person.CPF);
                else
                    return null;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao atualizar pessoa:" + e.Message);
                return null;
            }
        }

        public Task<bool> DeleteAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}