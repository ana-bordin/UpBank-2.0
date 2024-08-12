using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPBank.Person.Domain.Contracts;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.CommonsFiles.Contracts;

namespace UPBank.Person.Domain.Commands.CreatePerson
{
    internal class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CreatePersonCommandResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAddressService _addressService;
        private IMapper _mapper;
        public readonly IDomainNotificationService _domainNotificationService;

        public CreatePersonCommandHandler(IPersonRepository personRepository, IAddressService addressService, IMapper mapper, IDomainNotificationService domainNotificationService)
        {
            _personRepository = personRepository;
            _addressService = addressService;
            _mapper = mapper;
            _domainNotificationService = domainNotificationService;
        }

        public async Task<CreatePersonCommandResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        { 
            var addressResponse = await _addressService.CreateAddress(request.Address, cancellationToken);  
            var result = _domainNotificationService.Get();
            var person = _mapper.Map<Entities.Person>(request); 
            person.AddressId = addressResponse.Id;

            person = await _personRepository.CreatePerson(person);

            var mapperPerson = _mapper.Map<CreatePersonCommandResponse>(person);
            mapperPerson.Address = addressResponse;

            mapperPerson.Errors = _domainNotificationService.Get();
            return mapperPerson;
        }
    }
}