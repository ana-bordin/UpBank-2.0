﻿using Dapper;
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

        public async Task<(Domain.Entities.Employee employee, string message)> CreateEmployee(string cpf, bool manager)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var rows = await db.ExecuteAsync("INSERT INTO Employee (CPF, Manager) VALUES (@cpf, @manager)", new { CPF = cpf, Manager = manager });
                    if (rows > 0)
                        return await GetEmployeeByCpf(cpf);
                    else
                        return (null, null);
                }
            }
            catch (Exception e)
            {
                return (null, "houve um erro ao criar funcionario:" + e);
            }
        }

        public async Task<(bool ok, string message)> DeleteEmployeeByCpf(string cpf)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var rows = await db.ExecuteAsync("UPDATE dbo.Employee SET Active = 1 WHERE CPF = @CPF", new { CPF = cpf });
                    if (rows != null)
                        return (true, null);
                    else
                        return (false, null);
                }
            }
            catch (Exception e)
            {
                return (false, "houve um erro ao excluir usuario:" + e);
            }

        }

        public async Task<(IEnumerable<Domain.Entities.Employee> employees, string message)> GetAllEmployees()
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

        public async Task<(Domain.Entities.Employee employee, string message)> GetEmployeeByCpf(string cpf)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var employeeResult = await db.QueryFirstOrDefaultAsync<Domain.Entities.Employee>("SELECT * FROM Employee WHERE CPF = @cpf", new { CPF = cpf });
                    return (employeeResult, null);
                }
            }
            catch (Exception e)
            {
                return (null, "houve um erro ao buscar funcionario:" + e);
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

    }
}