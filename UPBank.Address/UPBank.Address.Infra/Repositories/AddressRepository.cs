using Dapper;
using UPBank.Address.Domain.Entities;
using UPBank.Address.Infra.Context;

namespace UPBank.Address.Infra.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IUpBankApiAddressContext _context;
        public AddressRepository(IUpBankApiAddressContext context)
        {
            _context = context;
        }

        public async Task<(CompleteAddress completeAddress, string message)> GetCompleteAddressById(Guid id)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var address = await db.QueryFirstOrDefaultAsync<CompleteAddress>("SELECT * FROM dbo.CompleteAddress AS ca INNER JOIN dbo.Address AS a ON ca.ZipCode = a.ZipCode WHERE ca.Id = @Id ", new { Id = id });

                    return (address, null);
                }
            }
            catch (Exception e)
            {
                return (null, "Erro ao buscar endereço completo pelo Id: " + e);
            }
        }

        public async Task<(Domain.Entities.Address address, string message)> GetAddressByZipCode(string zipCode)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var address = await db.QueryFirstOrDefaultAsync<Domain.Entities.Address>("SELECT ZipCode, Street, Neighborhood, City, State FROM dbo.Address WHERE ZipCode = @ZipCode", new { ZipCode = zipCode });

                    return (address, null);
                }
            }
            catch (Exception e)
            {
                return (null, "Erro ao buscar endereço pelo CEP: " + e);
            }

        }

        public async Task<(Domain.Entities.Address address, string message)> CreateAddress(Domain.Entities.Address address)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    var rows = await db.ExecuteAsync("INSERT INTO dbo.Address (ZipCode, Street, Neighborhood, City, State) VALUES (@ZipCode, @Street, @Neighborhood, @City, @State)", new { ZipCode = address.ZipCode, Street = address.Street, Neighborhood = address.Neighborhood, City = address.City, State = address.State });

                    return await GetAddressByZipCode(address.ZipCode);
                }
            }
            catch (Exception e)
            {
                return (null, "Erro ao criar endereço: " + e);
            }
        }

        public async Task<(bool ok, string message)> CreateCompleteAddress(CompleteAddress completeAddress)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    await db.ExecuteAsync("INSERT INTO dbo.CompleteAddress (Id, ZipCode, Complement, Number) VALUES (@Id, @ZipCode, @Complement, @Number)", new { Id = completeAddress.Id, ZipCode = completeAddress.ZipCode, Complement = completeAddress.Complement, Number = completeAddress.Number });

                    return (true, null);
                }
            }
            catch (Exception e)
            {
                return (false, "Erro ao criar endereço completo: " + e);
            }
        }

        public async Task<(CompleteAddress completeAddress, string message)> UpdateAddress(Guid id, CompleteAddress completeAddress)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    await db.ExecuteAsync("UPDATE dbo.CompleteAddress SET Complement = @Complement, ZipCode = @ZipCode, Number = @Number WHERE Id = @Id", new { Id = id, Complement = completeAddress.Complement, ZipCode = completeAddress.ZipCode, Number = completeAddress.Number });

                    return await GetCompleteAddressById(id);
                }
            }
            catch (Exception e)
            {
                return (null, "Erro ao atualizar endereço: " + e);
            }
        }

        public async Task<(bool ok, string message)> DeleteAddressById(Guid id)
        {
            try
            {
                using (var db = _context.Connection)
                {
                    await db.ExecuteAsync("DELETE FROM dbo.CompleteAddress WHERE Id = @Id", new { Id = id });

                    return (true, null);
                }
            }
            catch (Exception e)
            {
                return (false, "Erro ao deletar endereço: " + e);
            }

        }
    }
}
