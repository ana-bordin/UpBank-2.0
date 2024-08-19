using MediatR;
using UPBank.Address.Domain.Commands.CreateAddress;

namespace UPBank.Address.Domain.Commands.UpdateAddress
{
    public class UpdateAddressCommand : CreateAddressCommand, IRequest<CreateAddressCommandResponse>
    {
        public Guid Id { get; set; }
    }
}