using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Commands.UpdateAddress;

namespace UPBank.Utils.Integration.Address.Contracts
{
    public interface IAddressService
    {
        Task<CreateAddressCommandResponse?> CreateAddress(CreateAddressCommand createAddress);
        Task<CreateAddressCommandResponse?> UpdateAddress(string id, UpdateAddressCommand updateAddress);
        Task<CreateAddressCommandResponse?> GetCompleteAddressById(string id);
    }
}