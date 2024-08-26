using Dapper;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Customer.Infra.Context;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Customer.Infra.Repostories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IUpBankApiCustomerContext _context;
        private readonly IDomainNotificationService _domainNotificationService;

        public CustomerRepository(IUpBankApiCustomerContext context, IDomainNotificationService domainNotificationService)
        {
            _context = context;
            _domainNotificationService = domainNotificationService;
        }

        public async Task<Domain.Entities.Customer> AddAsync(Domain.Entities.Customer customer)
        {
            try
            {
                await _context.Connection.ExecuteAsync("INSERT INTO dbo.Customer (CPF) VALUES (@CPF)", new { CPF = customer.CPF });

                return await GetOneAsync(customer.CPF);
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao criar o cliente: " + e.Message);
                return null;
            }
        }

        public async Task<Domain.Entities.Customer> GetOneAsync(string cpf)
        {
            try
            {
                var customer = await _context.Connection.QueryFirstOrDefaultAsync<Domain.Entities.Customer>("SELECT * FROM dbo.Customer WHERE CPF = @CPF", new { CPF = cpf });

                return customer;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao buscar o cliente: " + e.Message);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(string cpf)
        {
            try
            {
                var rows = await _context.Connection.ExecuteAsync("UPDATE dbo.Customer SET Active = 0 WHERE CPF = @CPF", new { CPF = cpf });
                return true;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao deletar o cliente: " + e.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Domain.Entities.Customer>> GetAllAsync()
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var customers = await db.QueryAsync<Domain.Entities.Customer>("SELECT * FROM dbo.Customer WHERE Active = 0");

                    return customers;
                }
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao buscar os clientes: " + e.Message);
                return null;
            }
        }

        public async Task<Domain.Entities.Customer> CustomerPatchRestriction(string cpf)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var restriction = db.QueryFirstOrDefault<int>("SELECT Restriction FROM dbo.Customer WHERE CPF = @CPF", new { CPF = cpf });
                    var customer = new Domain.Entities.Customer();

                    if (restriction == 1)
                        customer = db.QueryFirstOrDefault<Domain.Entities.Customer>("UPDATE dbo.Customer SET Restriction = 0 WHERE CPF = @CPF; SELECT * FROM dbo.Customer WHERE CPF = @CPF", new { CPF = cpf });

                    else
                        customer = db.QueryFirstOrDefault<Domain.Entities.Customer>("UPDATE dbo.Customer SET Restriction = 1 WHERE CPF = @CPF; SELECT * FROM dbo.Customer WHERE CPF = @CPF", new { CPF = cpf });

                    return (customer);
                }
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao modificar a restrição do cliente: " + e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Domain.Entities.Customer>> GetAllCustomersWithRestriction()
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var customers = await db.QueryAsync<Domain.Entities.Customer>("SELECT * FROM dbo.Customer WHERE Restriction = 1");

                    return customers;
                }
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao buscar os clientes com restrição: " + e.Message);
                return null;
            }
        }

        public async Task<bool> AccountOpening(List<string> cpfs)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var rows = await db.ExecuteAsync("INSERT INTO dbo.CustomerRequest (First, Second) VALUES (@First, @Second)", new { First = cpfs[0], Second = cpfs[1] });

                    return true;
                }
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Houve um erro ao abrir a conta: " + e.Message);
                return false;
            }

        }

        public Task<Domain.Entities.Customer?> UpdateAsync(Domain.Entities.Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
