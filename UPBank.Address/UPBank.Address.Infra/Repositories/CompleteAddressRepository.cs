using Dapper;
using UPBank.Address.Domain.Entities;
using UPBank.Address.Infra.Context;
using UPBank.Utils.CommonsFiles.Contracts;

namespace UPBank.Address.Infra.Repositories
{
    public class CompleteAddressRepository : IRepository<CompleteAddress>
    {
        private readonly IUpBankApiAddressContext _context;
        private readonly IDomainNotificationService _domainNotificationService;
        public CompleteAddressRepository(IUpBankApiAddressContext context, IDomainNotificationService domainNotificationService)
        {
            _context = context;
            _domainNotificationService = domainNotificationService;
        }

        public async Task<CompleteAddress?> GetOneAsync(string id)
        {
            try
            {
                var completeAddress = await _context.Connection.QueryFirstOrDefaultAsync<CompleteAddress>("SELECT Id, ZipCode, Complement, Number FROM dbo.CompleteAddress WHERE Id = @Id", new { Id = id });

                return completeAddress;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Não foi possível buscar o dado no banco" + e);
                return null;
            }
        }
        public async Task<CompleteAddress?> AddAsync(CompleteAddress completeAddress)
        {
            try
            {
                await _context.Connection.ExecuteAsync("INSERT INTO dbo.CompleteAddress (Id, ZipCode, Complement, Number) VALUES (@Id, @ZipCode, @Complement, @Number)", new { Id = completeAddress.Id, ZipCode = completeAddress.ZipCode, Complement = completeAddress.Complement, Number = completeAddress.Number });

                return await GetOneAsync(completeAddress.Id.ToString());
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Não foi possível salvar o dado no banco" + e);
                return null;
            }
        }

        public async Task<CompleteAddress?> UpdateAsync(CompleteAddress completeAddress)
        {
            try
            {
                await _context.Connection.ExecuteAsync("UPDATE dbo.CompleteAddress SET Complement = @Complement, ZipCode = @ZipCode, Number = @Number WHERE Id = @Id", new { Id = completeAddress.Id, Complement = completeAddress.Complement, ZipCode = completeAddress.ZipCode, Number = completeAddress.Number });

                return await GetOneAsync(completeAddress.Id.ToString());
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Não foi possível atualizar o dado no banco" + e);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                await _context.Connection.ExecuteAsync("DELETE FROM dbo.CompleteAddress WHERE Id = @Id", new { Id = id });
                return true;
            }
            catch (Exception e)
            {
                _domainNotificationService.Add("Não foi possível deletar o dado no banco" + e);
                return false;
            }
        }

        public Task<IEnumerable<CompleteAddress>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
