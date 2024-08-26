using AutoMapper;
using MediatR;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Person.Domain.Contracts;
using UPBank.Utils.Integration.Address.Contracts;

namespace UPBank.Person.Domain.Queries.GetPersonByCPF
{
    public class GetPersonByCPFQueryHandler : IRequestHandler<GetPersonByCPFQuery, CreatePersonCommandResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;

        public GetPersonByCPFQueryHandler(IPersonRepository personRepository, IMapper mapper, IAddressService addressService)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _addressService = addressService;
        }
        public async Task<CreatePersonCommandResponse> Handle(GetPersonByCPFQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetPersonByCpf(request.CPF);

            if (person == null)
                return await Task.FromResult<CreatePersonCommandResponse>(null);

            var address = await _addressService.GetCompleteAddressById(person.AddressId.ToString());

            var response = _mapper.Map<CreatePersonCommandResponse>(person);
            response.Address = address;

            return response;
        }
    }
}