using Dapper;
using UPBank.Employee.Domain.Contracts;
using UPBank.Employee.Infra.Context;

namespace UPBank.Employee.Infra.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IUpBankApiEmployeeContext _context;

        public EmployeeRepository(IUpBankApiEmployeeContext context)
        {
            _context = context;
        }

        public async Task<(Domain.Entities.Employee? entity, string message)> AddAsync(Domain.Entities.Employee employee)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var rows = await db.ExecuteAsync("INSERT INTO Employee (CPF, Manager, RecordNumber) VALUES (@cpf, @manager, @recordNumber)", new { CPF = employee.CPF, Manager = employee.Manager, RecordNumber = employee.RecordNumber });

                    return await GetOneAsync(employee.CPF);
                }
            }
            catch (Exception e)
            {
                return (null, "houve um erro ao criar funcionario:" + e);
            }
        }
        public async Task<(bool ok, string message)> DeleteAsync<TKey>(TKey key)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var rows = await db.ExecuteAsync("UPDATE dbo.Employee SET Active = 1 WHERE CPF = @CPF", new { CPF = key });

                    return (true, null);
                }
            }
            catch (Exception e)
            {
                return (false, "houve um erro ao excluir usuario:" + e);
            }

        }

        public async Task<(IEnumerable<Domain.Entities.Employee> entities, string message)> GetAllAsync()
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var employees = await db.QueryAsync<Domain.Entities.Employee>("SELECT * FROM Employee");
                    return (employees, null);
                }
            }
            catch (Exception e)
            {
                return (null, "houve um erro ao buscar funcionarios:" + e);
            }
        }

        public async Task<(Domain.Entities.Employee? entity, string message)> GetOneAsync<TKey>(TKey key)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var employeeResult = await db.QueryFirstOrDefaultAsync<Domain.Entities.Employee>("SELECT * FROM Employee WHERE CPF = @cpf", new { CPF = key });
                    return (employeeResult, null);
                }
            }
            catch (Exception e)
            {
                return (null, "houve um erro ao buscar funcionario:" + e);
            }

        }

        public async Task<(Domain.Entities.Employee? entity, string message)> UpdateAsync(Domain.Entities.Employee entity)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var rows = await db.ExecuteAsync("UPDATE dbo.Employee SET Manager = @Manager WHERE CPF = @CPF", new { CPF = entity.CPF, Manager = entity.Manager });

                    return await GetOneAsync(entity.CPF);
                }
            }
            catch (Exception e)
            {
                return (null, "houve um erro ao atualizar funcionario:" + e);
            }
        }




        public Task<Domain.Entities.Employee> SetProfile(string cpf, bool manager)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Employee> ApproveAccountOpening(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Employee> AccountOpeningRequests()
        {
            throw new NotImplementedException();
        }
    }
}
