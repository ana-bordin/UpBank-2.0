﻿using Dapper;
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

        public async Task<bool> CreateAddress(Domain.Entities.Address address)
        {
            var ifExists = await GetAddressByZipCode(address.ZipCode);

            using (var db = _context.Connection)
            {
                var rows = 0;
                if (ifExists == null)
                {
                    rows += await db.ExecuteAsync("INSERT INTO dbo.Address (ZipCode, Street, Neighborhood, City, State) VALUES (@ZipCode, @Street, @Neighborhood, @City, @State)", new { ZipCode = address.ZipCode, Street = address.Street, Neighborhood = address.Neighborhood, City = address.City, State = address.State });
                    return true;
                }

                else
                    return false;
                //return Task.FromResult(address).Result;
            }
        }

        public async Task<Guid> CreateCompleteAddress(Domain.Entities.CompleteAddress addressInputModel)
        {

            using (var db = _context.Connection)
            {
                Guid id = Guid.NewGuid();
                var rows = 0;

                rows += await db.ExecuteAsync("INSERT INTO dbo.CompleteAddress (Id, ZipCode, Complement, Number) VALUES (@Id, @ZipCode, @Complement, @Number)", new { Id = id, ZipCode = addressInputModel.ZipCode, Complement = addressInputModel.Complement, Number = addressInputModel.Number });

                if (rows > 0)
                    return id;
                else
                    return Guid.Empty;
            }
        }

        public async Task<Domain.Entities.CompleteAddress> GetCompleteAddressById(Guid id)
        {
            using (var db = _context.Connection)
            {
                return await db.QueryFirstOrDefaultAsync<Domain.Entities.CompleteAddress>("SELECT * FROM dbo.CompleteAddress AS ca INNER JOIN dbo.Address AS a ON ca.ZipCode = a.ZipCode WHERE ca.Id = @Id ", new { Id = id });
            }
        }
        public async Task<Domain.Entities.Address> GetAddressByZipCode(string zipCode)
        {
            using (var db = _context.Connection)
            {
                var address = await db.QueryFirstOrDefaultAsync<Domain.Entities.Address>("SELECT ZipCode, Street, Neighborhood, City, State FROM dbo.Address WHERE ZipCode = @ZipCode", new { ZipCode = zipCode });

                if (address != null)
                    return address;

                else
                    return null;
            }
        }

        public async Task<Domain.Entities.CompleteAddress> UpdateAddress(Guid id, Domain.Entities.CompleteAddress addressInputModel)
        {
            using (var db = _context.Connection)
            {
                var rows = await db.ExecuteAsync("UPDATE dbo.CompleteAddress SET Complement = @Complement, ZipCode = @ZipCode, Number = @Number WHERE Id = @Id", new { Id = id, Complement = addressInputModel.Complement, ZipCode = addressInputModel.ZipCode, Number = addressInputModel.Number });

                if (rows > 0)
                    return await GetCompleteAddressById(id);
                else
                    return null;
            }
        }

        public async Task<bool> DeleteAddressById(Guid id)
        {
            using (var db = _context.Connection)
            {
                var rows = await db.ExecuteAsync("DELETE FROM dbo.CompleteAddress WHERE Id = @Id", new { Id = id });

                if (rows > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
