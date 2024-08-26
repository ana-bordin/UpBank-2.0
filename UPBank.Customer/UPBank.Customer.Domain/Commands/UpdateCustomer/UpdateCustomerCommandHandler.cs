using AutoMapper;
using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.Integration.Person.Contracts;

namespace UPBank.Customer.Domain.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CreateCustomerCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly IPersonService _personService;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IDomainNotificationService domainNotificationService, IPersonService personService)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _domainNotificationService = domainNotificationService;
            _personService = personService;
        }

        public async Task<CreateCustomerCommandResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _personService.UpdatePersonAsync(request.CPF, request);
            
            return _mapper.Map<CreatePersonCommandResponse, CreateCustomerCommandResponse>(result);
        }
    }
}
