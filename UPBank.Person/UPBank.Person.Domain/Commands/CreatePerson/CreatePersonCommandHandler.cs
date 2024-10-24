using AutoMapper;
using MediatR;
using UPBank.Person.Domain.Contracts.Repositories;
using UPBank.Person.Domain.Contracts.Services;

namespace UPBank.Person.Domain.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CreatePersonCommandResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAddressServiceClient _addressServiceClient;
        private IMapper _mapper;

        public CreatePersonCommandHandler(IPersonRepository personRepository, IAddressServiceClient addressServiceClient, IMapper mapper)
        {
            _personRepository = personRepository;
            _addressServiceClient = addressServiceClient;
            _mapper = mapper;
        }

        public async Task<CreatePersonCommandResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var personExists = await _personRepository.GetPersonByCpf(request.CPF);
            if (personExists == null)
            {
                var addressResponse = await _addressServiceClient.CreateAddress(request.Address);

                var person = _mapper.Map<Entities.Person.Person>(request);
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
            else
            {
                return _mapper.Map<CreatePersonCommandResponse>(personExists);
            }
        }
    }
}