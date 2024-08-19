using MediatR;
using UPBank.Address.Domain.Commands.CreateAddress;

namespace UPBank.Address.Domain.Queries.GetAddressById
{
    public class GetAddressByIdQuery : IRequest<CreateAddressCommandResponse>
    {
        public Guid Id { get; set; }

        public GetAddressByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}