using MediatR;

namespace UPBank.Address.Domain.Commands.CreateAddress
{
    public class CreateAddressCommand : IRequest<CreateAddressCommandResponse>
    {
        public string ZipCode { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Complement { get; set; }
    }
}
