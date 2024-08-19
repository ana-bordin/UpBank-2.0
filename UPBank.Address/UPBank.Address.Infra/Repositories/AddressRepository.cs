using Dapper;
using UPBank.Address.Domain.Contracts;
using UPBank.Address.Infra.Context;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Address.Infra.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IUpBankApiAddressContext _context;
        private readonly IDomainNotificationService _domainNotificationService;
        public AddressRepository(IUpBankApiAddressContext context, IDomainNotificationService domainNotificationService)
        {
            _context = context;
            _domainNotificationService = domainNotificationService;
        }

        public async Task<Domain.Entities.Address?> GetOneAsync(string zipCode)
        {
            try
            {
                var address = await _context.Connection.QueryFirstOrDefaultAsync<Domain.Entities.Address>("SELECT ZipCode, Street, Neighborhood, City, State FROM dbo.Address WHERE ZipCode = @ZipCode", new { ZipCode = zipCode });

                return address;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Erro ao buscar endereço pelo CEP: " + e);
                return null;
            }
        }

        public async Task<Domain.Entities.Address?> AddAsync(Domain.Entities.Address address)
        {
            try
            {
                await _context.Connection.ExecuteAsync("INSERT INTO dbo.Address (ZipCode, Street, Neighborhood, City, State) VALUES (@ZipCode, @Street, @Neighborhood, @City, @State)", new { ZipCode = address.ZipCode, Street = address.Street, Neighborhood = address.Neighborhood, City = address.City, State = address.State });
                return await GetOneAsync(address.ZipCode);
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Erro ao criar endereço: " + e);
                return null;
            }
        }
    }
}