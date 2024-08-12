using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Commands.UpdateAddress;

namespace UPBank.Utils.Address.Contracts
{
    public interface IAddressService
    {
        Task<CreateAddressCommandResponse?> CreateAddress(CreateAddressCommand createAddressCommand, CancellationToken cancellationToken);
        Task<CreateAddressCommandResponse?> UpdateAddress(string id, UpdateAddressCommand updateAddressCommand, CancellationToken cancellationToken);
        Task<CreateAddressCommandResponse?> GetCompleteAddressById(string id, CancellationToken cancellationToken);
    }
}