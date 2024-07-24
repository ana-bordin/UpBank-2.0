using UPBank.Address.Application.Models;
using UPBank.Address.Domain.Entities;

namespace UPBank.Utils.Address.Contracts
{
    public interface IAddressService
    {
        Task<(AddressOutputModel addressOutputModel, string message)> CreateAddress(AddressInputModel addressInputModel);
        Task<(CompleteAddress completeAddress, string message)> UpdateAddress(Guid Id, AddressInputModel addressInputModel);
        Task<(AddressOutputModel addressOutputModel, string message)> GetCompleteAddressById(Guid id);
    }
}
