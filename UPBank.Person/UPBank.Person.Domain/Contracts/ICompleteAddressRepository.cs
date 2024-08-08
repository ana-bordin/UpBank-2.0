using UPBank.Address.Domain.Entities;
using UPBank.Utils.CommonsFiles;

namespace UPBank.Person.Domain.Contracts
{
    public interface ICompleteAddressRepository : IRepository<CompleteAddress>
    {
        Task<(bool ok, string message)> CreateCompleteAddress(CompleteAddress completeAddress); 
        Task<(CompleteAddress completeAddress, string message)> GetCompleteAddressById(Guid id);
        Task<(CompleteAddress completeAddress, string message)> UpdateAddress(Guid id, CompleteAddress completeAddress);
    }
}
