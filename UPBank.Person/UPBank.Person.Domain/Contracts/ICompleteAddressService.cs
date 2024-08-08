using UPBank.Address.Application.Models;
using UPBank.Address.Domain.Entities;

namespace UPBank.Address.Application.Contracts
{
    public interface ICompleteAddressService
    {
        Task<(bool ok, string message)> CreateAddress(string zipCode);
        Task<(Guid guid, string message)> CreateCompleteAddress(AddressInputModel addressInputModel);
        Task<(AddressOutputModel addressOutputModel, string message)> GetCompleteAddressById(Guid id);
        Task<(Domain.Entities.Address address, string message)> GetAddressByZipCode(string zipCode);
        Task<(AddressOutputModel addressOutputModel, string message)> UpdateAddress(Guid id, AddressInputModel addressInputModel);
        Task<(bool ok, string message)> DeleteAddressById(Guid id);
        Task<AddressOutputModel> CreateAddressOutputModel(CompleteAddress completeAddress, Domain.Entities.Address address);
    }
}
