using MediatR;
using UPBank.Address.Domain.DTOs;

namespace UPBank.Address.Domain.Commands.CreateAddress
{
    public class CreateAddressCommand : ResponseDTO, IRequest<CreateAddressCommandResponse>
    {
        public string ZipCode { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Complement { get; set; }
    }
}
