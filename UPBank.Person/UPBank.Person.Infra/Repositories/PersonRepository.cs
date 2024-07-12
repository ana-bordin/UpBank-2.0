using Dapper;
using UPBank.Person.Application.Models.DTOs;
using UPBank.Person.Domain.Contracts;
using UPBank.Person.Infra.Context;

namespace UPBank.Person.Infra.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IUpBankApiPersonContext _context;

        public PersonRepository(IUpBankApiPersonContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePerson(Domain.Entities.Person person)
        {
            using (var db = _context.Connection)
            {
                var rows = await db.ExecuteAsync("INSERT INTO dbo.Person (Name, BirthDate, CPF, Email, Phone, Gender, Salary, AddressId) VALUES (@Name, @BirthDate, @CPF, @Email, @Phone, @Gender, @Salary, @AddressId)", new { Name = person.Name, BirthDate = person.BirthDate, CPF = person.CPF, Email = person.Email, Phone = person.Phone, Gender = person.Gender, Salary = person.Salary, AddressId = person.AddressId });

                if (rows > 0)
                    return true;
                else
                    return false;
            }
        }

        public async Task<Domain.Entities.Person> GetPersonByCpf(string cpf)
        {
            using (var db = _context.Connection)
            {
                try
                {
                    var person = await db.QueryFirstOrDefaultAsync<Domain.Entities.Person>("SELECT * FROM dbo.Person WHERE CPF = @CPF", new { CPF = cpf });
                    return person;
                }
                catch (Exception)
                {
                    throw new Exception("Person not found");
                }
            }
        }

        public async Task<bool> CheckIfExist(string cpf)
        {
            using (var db = _context.Connection)
            {
                var person = await db.QueryFirstOrDefaultAsync<Domain.Entities.Person>("SELECT * FROM dbo.Person WHERE CPF = @CPF", new { CPF = cpf });
                if (person == null)
                    return true;
                return false;
            }
        }
        public async Task<Domain.Entities.Person> PatchPerson(string cpf, Domain.Entities.Person person)
        {
            using (var db = _context.Connection)
            {
                var rows = await db.ExecuteAsync("UPDATE dbo.Person SET Name = @Name, Email = @Email, Phone = @Phone, Gender = @Gender, Salary = @Salary WHERE CPF = @CPF", new { Name = person.Name, Email = person.Email, Phone = person.Phone, Gender = person.Gender, Salary = person.Salary, CPF = cpf });

                if (rows > 0)
                    return await GetPersonByCpf(person.CPF);
                else
                    throw new NotImplementedException("Erro ao atualizar o cliente.");
            }
        }
    }
}
