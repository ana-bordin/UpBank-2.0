using AutoMapper;
using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.Integration.Person.Contracts;

namespace UPBank.Customer.Domain.Queries.GetCustomerByCPF
{
    public class GetCustomerByCPFQueryHandler : IRequestHandler<GetCustomerByCPFQuery, CreateCustomerCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;
        private readonly IDomainNotificationService _domainNotificationService;

        public GetCustomerByCPFQueryHandler(ICustomerRepository customerRepository, IMapper mapper, IDomainNotificationService domainNotificationService, IPersonService personService)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _personService = personService;
            _domainNotificationService = domainNotificationService;
        }
        public async Task<CreateCustomerCommandResponse> Handle(GetCustomerByCPFQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetOneAsync(request.CPF);

            if (customer == null || customer.Active == false)
            {
                if (!_domainNotificationService.HasNotification)
                    _domainNotificationService.Add("Cliente não existe!");

                return null;
            }

            var person = await _personService.GetPersonByCPFAsync(request.CPF);

            var result = _mapper.Map<Entities.Customer, CreateCustomerCommandResponse>(customer);
            _mapper.Map(person, result);

            return result;
        }
    }
}
