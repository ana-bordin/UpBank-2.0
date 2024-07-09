using Dapper;
using UPBank.Customer.Application.Models.DTOs;
using UPBank.Customer.Domain.Contracts;
using UPBank.Customer.Infra.Context;

namespace UPBank.Customer.Infra.Repostories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IUpBankApiCustomerContext _context;

        public PersonRepository(IUpBankApiCustomerContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePerson(Domain.Entities.Person person)
        {
            using (var db = _context.ConnectionPerson)
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
            using (var db = _context.ConnectionPerson)
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
            using (var db = _context.ConnectionPerson)
            {
                var person = await db.QueryFirstOrDefaultAsync<Domain.Entities.Person>("SELECT * FROM dbo.Person WHERE CPF = @CPF", new { CPF = cpf });
                if (person == null)
                    return true;
                return false;
            }
        }
        public async Task<bool> PatchPerson(string cpf, PersonPatchDTO personPatchDTO)
        {
            using (var db = _context.ConnectionPerson)
            {
                var rows = await db.ExecuteAsync("UPDATE dbo.Person SET Name = @Name, Email = @Email, Phone = @Phone, Gender = @Gender, Salary = @Salary WHERE CPF = @CPF", new { Name = personPatchDTO.Name, Email = personPatchDTO.Email, Phone = personPatchDTO.Phone, Gender = personPatchDTO.Gender, Salary = personPatchDTO.Salary, CPF = cpf });
                if (rows > 0)
                    return true;
                else
                    throw new NotImplementedException("Erro ao atualizar o cliente.");
            }
        }

    }
}
