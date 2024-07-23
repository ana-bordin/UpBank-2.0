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

        public async Task<(Domain.Entities.Customer customer, string message)> CreateCustomer(string cpf)
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var rows = await db.ExecuteAsync("INSERT INTO dbo.Customer (CPF) VALUES (@CPF)", new { CPF = cpf });
                    if (rows > 0)
                        return await GetCustomerByCpf(cpf);
                    else
                        return (null, null);
                }
            }
            catch (Exception e)
            {
                return (null, "Houve um erro ao criar o cliente: " + e.Message);
            }
        }

        public async Task<(Domain.Entities.Customer customer, string message)> GetCustomerByCpf(string cpf)
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var customer = await db.QueryFirstOrDefaultAsync<Domain.Entities.Customer>("SELECT * FROM dbo.Customer WHERE CPF = @CPF", new { CPF = cpf });
                    return (customer, null);
                }
            }
            catch (Exception e)
            {
                return (null, "Houve um erro ao buscar o cliente: " + e.Message);
            }
        }

        public async Task<(bool ok, string message)> DeleteCustomerByCpf(string cpf)
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var rows = db.ExecuteAsync("UPDATE dbo.Customer SET Active = 1 WHERE CPF = @CPF", new { CPF = cpf });
                    return (true, null);
                }
            }
            catch (Exception e)
            {
                return (false, "Houve um erro ao deletar o cliente: " + e.Message);
            }
        }

        public async Task<(IEnumerable<Domain.Entities.Customer> customers, string message)> GetAllCustomers()
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var customers = await db.QueryAsync<Domain.Entities.Customer>("SELECT * FROM dbo.Customer WHERE Active = 0");
                    return (customers, null);
                }
            }
            catch (Exception e)
            {
                return (null, "Houve um erro ao buscar os clientes: " + e.Message);
            }

        }

        public async Task<(Domain.Entities.Customer customer, string message)> CustomerPatchRestriction(string cpf)
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var restriction = db.QueryFirstOrDefault<int>("SELECT Restriction FROM dbo.Customer WHERE CPF = @CPF", new { CPF = cpf });
                    var customer = new Domain.Entities.Customer();

                    if (restriction == 1)
                        customer = db.QueryFirstOrDefault<Domain.Entities.Customer>("UPDATE dbo.Customer SET Restriction = 0 WHERE CPF = @CPF; SELECT * FROM dbo.Customer WHERE CPF = @CPF", new { CPF = cpf });

                    else
                        customer = db.QueryFirstOrDefault<Domain.Entities.Customer>("UPDATE dbo.Customer SET Restriction = 1 WHERE CPF = @CPF; SELECT * FROM dbo.Customer WHERE CPF = @CPF", new { CPF = cpf });

                    return (customer, null);
                }
            }
            catch (Exception e)
            {
                return (null, "Houve um erro ao modificar a restrição do cliente: " + e.Message);
            }
        }

        public async Task<(IEnumerable<Domain.Entities.Customer> customers, string message)> GetAllCustomersWithRestriction()
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var customers = db.QueryAsync<Domain.Entities.Customer>("SELECT * FROM dbo.Customer WHERE Restriction = 1");
                    return (await customers, null);
                }
            }
            catch (Exception e)
            {
                return (null, "Houve um erro ao buscar os clientes com restrição: " + e.Message);
            }

        }

        public async Task<(bool ok, string message)> AccountOpening(List<string> cpfs)
        {
            try
            {
                using (var db = _context.ConnectionCustomer)
                {
                    var rows = await db.ExecuteAsync("INSERT INTO dbo.CustomerRequest (First, Second) VALUES (@First, @Second)", new { First = cpfs[0], Second = cpfs[1] });

                    if (rows > 0)
                        return (true, null);
                    else
                        return (false, null);
                }
            }
            catch (Exception e)
            {
                return (false, "Houve um erro ao abrir a conta: " + e.Message);
            }

        }
    }
}
