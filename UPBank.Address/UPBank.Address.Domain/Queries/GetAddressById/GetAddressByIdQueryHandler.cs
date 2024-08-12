using AutoMapper;
using MediatR;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Contracts;
using UPBank.Address.Domain.Entities;
using UPBank.Utils.CommonsFiles.Contracts;

namespace UPBank.Address.Domain.Queries.GetAddressById
{
    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, CreateAddressCommandResponse>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IRepository<CompleteAddress> _completeAddressRepository;
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly IMapper _mapper;

        public GetAddressByIdQueryHandler(IAddressRepository addressRepository, IRepository<CompleteAddress> completeAddressRepository, IDomainNotificationService domainNotificationService, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _completeAddressRepository = completeAddressRepository;
            _domainNotificationService = domainNotificationService;
            _mapper = mapper;
        }

        public async Task<CreateAddressCommandResponse> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var completeAddress = await _completeAddressRepository.GetOneAsync(request.Id.ToString());
            var address = new Entities.Address();

            if (completeAddress == null)
                _domainNotificationService.Add("Endereço não encontrado");
            else
                address = await _addressRepository.GetOneAsync(completeAddress.ZipCode);

            var result = _mapper.Map<CreateAddressCommandResponse>(address);
            _mapper.Map(completeAddress, result);

            return result;
        }
    }
}