using Dapper;
using UPBank.Employee.Domain.Contracts;
using UPBank.Employee.Infra.Context;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Employee.Infra.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IUpBankApiEmployeeContext _context;
        private readonly IDomainNotificationService _domainNotificationService;

        public EmployeeRepository(IUpBankApiEmployeeContext context, IDomainNotificationService domainNotificationService)
        {
            _context = context;
            _domainNotificationService = domainNotificationService;
        }

        public async Task<Domain.Entities.Employee> AddAsync(Domain.Entities.Employee employee)
        {
            try
            {
                await _context.Connection.ExecuteAsync("INSERT INTO Employee (CPF, Manager, RecordNumber) VALUES (@cpf, @manager, @recordNumber)", new { CPF = employee.CPF, Manager = employee.Manager, RecordNumber = employee.RecordNumber });

                return await GetOneAsync(employee.CPF);
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao adicionar funcionario:" + e);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(string cpf)
        {
            try
            {
                await _context.Connection.ExecuteAsync("UPDATE dbo.Employee SET Active = 1 WHERE CPF = @CPF", new { CPF = cpf });

                return true;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao deletar funcionario:" + e);
                return false;
            }

        }

        public async Task<IEnumerable<Domain.Entities.Employee>> GetAllAsync()
        {
            try
            {
                var employees = await _context.Connection.QueryAsync<Domain.Entities.Employee>("SELECT * FROM Employee");
                return employees;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao buscar funcionarios:" + e);
                return null;
            }
        }

        public async Task<Domain.Entities.Employee?> GetOneAsync(string cpf)
        {
            try
            {
                var employeeResult = await _context.Connection.QueryFirstOrDefaultAsync<Domain.Entities.Employee>("SELECT * FROM Employee WHERE CPF = @cpf", new { CPF = cpf });
                return employeeResult;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao buscar funcionario:" + e);
                return null;
            }

        }

        public async Task<Domain.Entities.Employee?> UpdateAsync(Domain.Entities.Employee entity)
        {
            try
            {
                var rows = await _context.Connection.ExecuteAsync("UPDATE dbo.Employee SET Manager = @Manager WHERE CPF = @CPF", new { CPF = entity.CPF, Manager = entity.Manager });

                return await GetOneAsync(entity.CPF);
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao atualizar funcionario:" + e);
                return null;
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
