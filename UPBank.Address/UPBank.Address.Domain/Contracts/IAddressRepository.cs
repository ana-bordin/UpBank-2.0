using UPBank.Address.Domain.Entities;

namespace UPBank.Address.Infra.Repositories
{
    public interface IAddressRepository
    {
        Task<bool> CreateAddress(Domain.Entities.Address address);
        Task<Guid> CreateCompleteAddress(CompleteAddress completeAddress);
        Task<CompleteAddress> GetCompleteAddressById(Guid id);
        Task<Domain.Entities.Address> GetAddressByZipCode(string zipCode);
        Task<CompleteAddress> UpdateAddress(Guid id, CompleteAddress addressDTO);
        Task<bool> DeleteAddressById(Guid id);
    }
}
