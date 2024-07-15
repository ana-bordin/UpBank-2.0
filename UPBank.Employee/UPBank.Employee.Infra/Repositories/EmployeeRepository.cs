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

        public async Task<Domain.Entities.Employee> CreateEmployee(string cpf, bool manager)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var rows = await db.ExecuteAsync("INSERT INTO Employee (CPF, Manager) VALUES (@cpf, @manager)", new { CPF = cpf, Manager = manager });
                    if (rows > 0)
                        return await GetEmployeeByCpf(cpf);
                    else
                        return null;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteEmployeeByCpf(string cpf)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var rows = await db.ExecuteAsync("UPDATE dbo.Employee SET Active = 1 WHERE CPF = @CPF", new { CPF = cpf });
                    if (rows != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Task<IEnumerable<Domain.Entities.Employee>> GetAllEmployees()
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var employees = db.QueryAsync<Domain.Entities.Employee>("SELECT * FROM Employee");
                    return employees;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<Domain.Entities.Employee> GetEmployeeByCpf(string cpf)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var employee = db.QueryFirstOrDefaultAsync<Domain.Entities.Employee>("SELECT * FROM Employee WHERE CPF = @cpf", new { CPF = cpf });
                    return employee;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
