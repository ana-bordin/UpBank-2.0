using AutoMapper;
using MediatR;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Contracts;
using UPBank.Address.Domain.Entities;
using UPBank.Utils.CommonsFiles.Contracts;

namespace UPBank.Address.Domain.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, CreateAddressCommandResponse>
    {
        public readonly IAddressRepository _addressRepository;
        public readonly IRepository<CompleteAddress> _completeAddressRepository;
        public readonly IMapper _mapper;
        public readonly IDomainNotificationService _domainNotificationService;
        private readonly CreateAddressCommandHandler _createAddressQueryHandler;

        public UpdateAddressCommandHandler(IAddressRepository addressRepository, IRepository<CompleteAddress> completeAddressRepository, IMapper mapper, IDomainNotificationService domainNotificationService, CreateAddressCommandHandler createAddressQueryHandler) : base()
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _completeAddressRepository = completeAddressRepository;
            _domainNotificationService = domainNotificationService;
            _createAddressQueryHandler = createAddressQueryHandler;
        }

        public async Task<CreateAddressCommandResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var completeAddress = _mapper.Map<UpdateAddressCommand, CompleteAddress>(request);

            var address = await _createAddressQueryHandler.HandleAddress(request);
            completeAddress.ZipCode = CreateAddressCommandProfile.GetOnlyNumbers(completeAddress.ZipCode);
            completeAddress = await _completeAddressRepository.UpdateAsync(completeAddress);

            if (completeAddress == null && _domainNotificationService.Get().Count() == 0)
                _domainNotificationService.Add("O endereço não existe");

            var result = _mapper.Map<CreateAddressCommandResponse>(address);
            _mapper.Map(completeAddress, result);

            result.Errors = _domainNotificationService.Get().ToList();
            return result;
        }
    }
}