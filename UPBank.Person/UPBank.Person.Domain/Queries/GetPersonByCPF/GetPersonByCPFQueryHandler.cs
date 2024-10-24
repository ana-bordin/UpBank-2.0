using AutoMapper;
using MediatR;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Person.Domain.Contracts.Repositories;
using UPBank.Person.Domain.Contracts.Services;

namespace UPBank.Person.Domain.Queries.GetPersonByCPF
{
    public class GetPersonByCPFQueryHandler : IRequestHandler<GetPersonByCPFQuery, CreatePersonCommandResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IAddressServiceClient _addressServiceClient;

        public GetPersonByCPFQueryHandler(IPersonRepository personRepository, IMapper mapper, IAddressServiceClient addressServiceClient)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _addressServiceClient = addressServiceClient;
        }
        public async Task<CreatePersonCommandResponse> Handle(GetPersonByCPFQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonByCpf(request.CPF);

            if (person == null)
                return await Task.FromResult<CreatePersonCommandResponse>(null);

            var address = await _addressServiceClient.GetCompleteAddressById(person.AddressId.ToString());

            var response = _mapper.Map<CreatePersonCommandResponse>(person);
            response.Address = address;

            return response;
        }
    }
}