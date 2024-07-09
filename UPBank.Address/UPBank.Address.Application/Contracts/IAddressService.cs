using UPBank.Address.Application.Models;
using UPBank.Address.Domain.Entities;

namespace UPBank.Address.Application.Contracts
{
    public interface IAddressService
    {
        Task<bool> CreateAddress(Domain.Entities.Address address);
        Task<Guid> CreateCompleteAddress(AddressInputModel addressInputModel);
        Task<CompleteAddress> GetCompleteAddressById(Guid id);
        Task<Domain.Entities.Address> GetAddressByZipCode(string zipCode);
        Task<CompleteAddress> UpdateAddress(Guid id, AddressInputModel addressInputModel);
        Task<bool> DeleteAddressById(Guid id);
    }
}
