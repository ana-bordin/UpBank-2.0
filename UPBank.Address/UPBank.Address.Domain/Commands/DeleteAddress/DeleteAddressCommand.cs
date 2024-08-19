using MediatR;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Address.Domain.Commands.DeleteAddress
{
    public class DeleteAddressCommand : IRequest<IDomainNotificationService>
    {
        public Guid Id { get; set; }

        public DeleteAddressCommand(Guid id)
        {
            Id = id;
        }
    }
}