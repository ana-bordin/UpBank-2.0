using AutoMapper;
using MediatR;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Person.Domain.Contracts.Repositories;
using UPBank.Person.Domain.Contracts.Services;

namespace UPBank.Person.Domain.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, CreatePersonCommandResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IAddressServiceClient _addressServiceClient;
        private IMapper _mapper;

        public UpdatePersonCommandHandler(IPersonRepository personRepository, IAddressServiceClient addressServiceClient, IMapper mapper)
        {
            _personRepository = personRepository;
            _addressServiceClient = addressServiceClient;
            _mapper = mapper;
        }
        public async Task<CreatePersonCommandResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            request.CPF = CreatePersonCommand.CpfRemoveMask(request.CPF);
            var person = await _personRepository.GetPersonByCpf(request.CPF);

            if (person == null)
                return null;

            else
            {
                var addressResponse = await _addressServiceClient.UpdateAddress(person.AddressId.ToString(), request.Address);

                person = _mapper.Map<Entities.Person.Person>(request);


                person = await _personRepository.PatchPerson(person.CPF, person);

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
}