using MediatR;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.DTOs;

namespace UPBank.Address.Domain.Queries.GetAddressById
{
    public class GetAddressByIdQuery : ResponseDTO, IRequest<CreateAddressCommandResponse>
    {
        public Guid Id { get; set; }
    }
}