using MediatR;
using UPBank.Address.Domain.Entities;
using UPBank.Utils.CommonsFiles.Contracts.Repositories;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Address.Domain.Commands.DeleteAddress
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, IDomainNotificationService>
    {

        private readonly IRepository<CompleteAddress> _completeAddressRepository;
        private readonly IDomainNotificationService _domainNotificationService;

        public DeleteAddressCommandHandler(IRepository<CompleteAddress> completeAddressRepository, IDomainNotificationService domainNotificationService)
        {
            _completeAddressRepository = completeAddressRepository;
            _domainNotificationService = domainNotificationService;
        }

        public async Task<IDomainNotificationService> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _completeAddressRepository.GetOneAsync(request.Id.ToString());
            if (address == null)
                _domainNotificationService.Add("Endereço não encontrado");
            else
                await _completeAddressRepository.DeleteAsync(request.Id.ToString());

            return _domainNotificationService;
        }
    }
}