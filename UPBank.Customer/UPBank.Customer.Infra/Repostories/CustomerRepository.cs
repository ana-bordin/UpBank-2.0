using Dapper;
using UPBank.Customer.Domain.Contracts;
using UPBank.Customer.Infra.Context;

namespace UPBank.Customer.Infra.Repostories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IUpBankApiCustomerContext _context;

        public CustomerRepository(IUpBankApiCustomerContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Customer> CreateCustomer(string cpf)
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var rows = await db.ExecuteAsync("INSERT INTO dbo.Customer (CPF) VALUES (@CPF)", new { CPF = cpf });
                    if (rows > 0)
                        return await GetCustomerByCpf(cpf);
                    else
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<Domain.Entities.Customer> GetCustomerByCpf(string cpf)
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var customer = await db.QueryFirstOrDefaultAsync<Domain.Entities.Customer>("SELECT * FROM dbo.Customer WHERE CPF = @CPF", new { CPF = cpf });
                    return customer;
                }
            }
            catch (Exception)
            {
               return null;
            }
        }

        public async Task<bool> DeleteCustomerByCpf(string cpf)
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var rows = db.Execute("UPDATE dbo.Customer SET Active = 1 WHERE CPF = @CPF", new { CPF = cpf });
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<IEnumerable<Domain.Entities.Customer>> GetAllCustomers()
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var customers = await db.QueryAsync<Domain.Entities.Customer>("SELECT * FROM dbo.Customer WHERE Active = 0");
                    return customers;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
