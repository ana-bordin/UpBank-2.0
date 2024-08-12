using AutoMapper;
using MediatR;
using UPBank.Address.Domain.Contracts;
using UPBank.Address.Domain.Entities;
using UPBank.Utils.CommonsFiles.Contracts;

namespace UPBank.Address.Domain.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>
    {
        public readonly IAddressRepository _addressRepository;
        public readonly IRepository<CompleteAddress> _completeAddressRepository;
        public readonly IMapper _mapper;
        public readonly IDomainNotificationService _domainNotificationService;
        private readonly IViaCepService _iViaCepService;

        public CreateAddressCommandHandler(IAddressRepository addressRepository, IRepository<CompleteAddress> completeAddressRepository, IMapper mapper, IDomainNotificationService domainNotificationService, IViaCepService iViaCepService) : base()
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _completeAddressRepository = completeAddressRepository;
            _domainNotificationService = domainNotificationService;
            _iViaCepService = iViaCepService;
        }
        async Task<CreateAddressCommandResponse> IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>.Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await HandleAddress(request);
            var completeAddress = new CompleteAddress();
            if (address != null)
                completeAddress = await HandleCompleteAddress(request);

            var result = _mapper.Map<CreateAddressCommandResponse>(address);
            _mapper.Map(completeAddress, result); 
            return result;
        }

        public async Task<Entities.Address> HandleAddress(CreateAddressCommand createAddressCommand)
        {
            createAddressCommand.ZipCode = CreateAddressCommandProfile.GetOnlyNumbers(createAddressCommand.ZipCode);
            var getAddress = await _addressRepository.GetOneAsync(createAddressCommand.ZipCode);

            if (getAddress != null)
                return getAddress;

            getAddress = await _iViaCepService.GetAddressByZipCode(createAddressCommand.ZipCode);

            getAddress.ZipCode = CreateAddressCommandProfile.GetOnlyNumbers(getAddress.ZipCode);
            var okAddress = await _addressRepository.AddAsync(getAddress);

            return okAddress;
        }

        private async Task<CompleteAddress> HandleCompleteAddress(CreateAddressCommand createAddressCommand)
        {
            var completeAddress = _mapper.Map<CreateAddressCommand, CompleteAddress>(createAddressCommand);
            return await _completeAddressRepository.AddAsync(completeAddress);
        }
    }
}