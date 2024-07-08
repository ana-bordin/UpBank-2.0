using UPBank.Address.Domain.Entities;

namespace UPBank.Address.Domain.Contracts
{
    public interface IAddressService
    {
        Task<bool> CreateAddress(Entities.Address address);
        Task<Guid> CreateCompleteAddress(CompleteAddress addressInputModel);
        Task<CompleteAddress> GetCompleteAddressById(Guid id);
        Task<Entities.Address> GetAddressByZipCode(string zipCode);
        Task<CompleteAddress> UpdateAddress(Guid id, CompleteAddress addressInputModel);
        Task<bool> DeleteAddressById(Guid id);
    }
}
