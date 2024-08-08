using UPBank.Address.Domain.Entities;
using UPBank.Address.Domain.Models;

namespace UPBank.Address.Domain.Contracts
{
    public interface IAddressService
    {
        Task<(bool ok, string message)> CreateAddress(string zipCode);
        Task<(Guid guid, string message)> CreateCompleteAddress(AddressInputModel addressInputModel);
        Task<(AddressOutputModel addressOutputModel, string message)> GetCompleteAddressById(Guid id);
        Task<(Entities.Address address, string message)> GetAddressByZipCode(string zipCode);
        Task<(AddressOutputModel addressOutputModel, string message)> UpdateAddress(Guid id, AddressInputModel addressInputModel);
        Task<(bool ok, string message)> DeleteAddressById(Guid id);
        Task<AddressOutputModel> CreateAddressOutputModel(CompleteAddress completeAddress, Entities.Address address);
    }
}
