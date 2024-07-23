using UPBank.Address.Domain.Entities;

namespace UPBank.Address.Infra.Repositories
{
    public interface IAddressRepository
    {
        Task<(Domain.Entities.Address address, string message)> CreateAddress(Domain.Entities.Address address);
        Task<(bool ok, string message)> CreateCompleteAddress(CompleteAddress completeAddress);
        Task<(CompleteAddress completeAddress, string message)> GetCompleteAddressById(Guid id);
        Task<(Domain.Entities.Address address, string message)> GetAddressByZipCode(string zipCode);
        Task<(CompleteAddress completeAddress, string message)> UpdateAddress(Guid id, CompleteAddress completeAddress);
        Task<(bool ok, string message)> DeleteAddressById(Guid id);
    }
}
