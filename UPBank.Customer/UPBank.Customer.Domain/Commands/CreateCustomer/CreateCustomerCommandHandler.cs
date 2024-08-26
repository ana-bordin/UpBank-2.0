using AutoMapper;
using MediatR;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.Integration.Person.Contracts;

namespace UPBank.Customer.Domain.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponseList>
    {
        private readonly IPersonService _personService;
        private readonly ICustomerRepository _customerRepository;
        private IMapper _mapper;
        private readonly IDomainNotificationService _domainNotificationService;

        public CreateCustomerCommandHandler(IPersonService personService, IMapper mapper, IDomainNotificationService domainNotificationService, ICustomerRepository customerRepository)
        {
            _personService = personService;
            _mapper = mapper;
            _domainNotificationService = domainNotificationService;
            _customerRepository = customerRepository;
        }

        async Task<CreateCustomerCommandResponseList> IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponseList>.Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var createCustomerCommandResponseList = new CreateCustomerCommandResponseList();
            createCustomerCommandResponseList.Customers = new List<CreateCustomerCommandResponse>();

            foreach (var person in request.Customers)
            {
                var personResponse = await _personService.CreatePersonAsync(person);
                if (personResponse == null)
                    return null;

                var customer = _mapper.Map<CreatePersonCommandResponse, Entities.Customer>(personResponse);
                customer = await _customerRepository.AddAsync(customer);

                var createCustomerCommandResponse = _mapper.Map<Entities.Customer, CreateCustomerCommandResponse>(customer);
                    
                _mapper.Map(personResponse, createCustomerCommandResponse);
                
                createCustomerCommandResponseList.Customers.Add(createCustomerCommandResponse);
            }
            return createCustomerCommandResponseList;
        }
    }
}