using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Commands.UpdateAddress;
using UPBank.Address.Domain.Queries.GetAddressById;

namespace UPBank.Utils.Address.Contracts
{
    public interface IAddressService
    {
        Task<CreateAddressCommandResponse> CreateAddress(CreateAddressCommand createAddressCommand, CancellationToken cancellationToken);
        Task<CreateAddressCommandResponse> UpdateAddress(Guid Id, UpdateAddressCommand updateAddressCommand, CancellationToken cancellationToken);
        Task<CreateAddressCommandResponse> GetCompleteAddressById(GetAddressByIdQuery getAddressByIdQuery, CancellationToken cancellationToken);
    }
}