using Dapper;
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
            try
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Domain.Entities.Person> GetPersonByCpf(string cpf)
        {
            try
            {
                using (var db = _context.Connection)
                {

                    var person = await db.QueryFirstOrDefaultAsync<Domain.Entities.Person>("SELECT * FROM dbo.Person WHERE CPF = @CPF", new { CPF = cpf });
                    return person;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Domain.Entities.Person> PatchPerson(string cpf, Domain.Entities.Person person)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var rows = await db.ExecuteAsync("UPDATE dbo.Person SET Name = @Name, Email = @Email, Phone = @Phone, Gender = @Gender, Salary = @Salary WHERE CPF = @CPF", new { Name = person.Name, Email = person.Email, Phone = person.Phone, Gender = person.Gender, Salary = person.Salary, CPF = cpf });

                    if (rows > 0)
                        return await GetPersonByCpf(person.CPF);
                    else
                        return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
