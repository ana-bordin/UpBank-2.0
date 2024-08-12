using AutoMapper;
using MediatR;
using UPBank.Person.Domain.Contracts;
using UPBank.Utils.Address.Contracts;

namespace UPBank.Person.Domain.Commands.CreatePerson
{
    internal class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CreatePersonCommandResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAddressService _addressService;
        private IMapper _mapper;

        public CreatePersonCommandHandler(IPersonRepository personRepository, IAddressService addressService, IMapper mapper)
        {
            _personRepository = personRepository;
            _addressService = addressService;
            _mapper = mapper;
        }

        public async Task<CreatePersonCommandResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var addressResponse = await _addressService.CreateAddress(request.Address, cancellationToken);

            var person = _mapper.Map<Entities.Person>(request);
            person.AddressId = addressResponse.Id;

            person = await _personRepository.CreatePerson(person);

            var mapperPerson = new CreatePersonCommandResponse();

            if (person != null)
            {
                mapperPerson = _mapper.Map<CreatePersonCommandResponse>(person);
                mapperPerson.Address = addressResponse;
            }

            return mapperPerson;
        }
    }
}