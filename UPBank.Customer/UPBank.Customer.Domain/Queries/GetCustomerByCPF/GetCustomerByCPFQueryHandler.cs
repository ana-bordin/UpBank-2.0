using AutoMapper;
using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer;
using UPBank.Customer.Domain.Contracts.Services;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Customer.Domain.Queries.GetCustomerByCPF
{
    public class GetCustomerByCPFQueryHandler : IRequestHandler<GetCustomerByCPFQuery, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IPersonServiceClient _personServiceClient;
        private readonly IDomainNotificationService _domainNotificationService;

        public GetCustomerByCPFQueryHandler(ICustomerRepository customerRepository, IMapper mapper, IDomainNotificationService domainNotificationService, IPersonServiceClient personServiceClient)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _personServiceClient = personServiceClient;
            _domainNotificationService = domainNotificationService;
        }
        public async Task<CustomerResponse> Handle(GetCustomerByCPFQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetOneAsync(request.CPF);

            if (customer == null || customer.Active == false)
            {
                if (!_domainNotificationService.HasNotification)
                    _domainNotificationService.Add("Cliente não existe!");

                return null;
            }

            var person = await _personServiceClient.GetPersonByCPFAsync(request.CPF);

            var result = _mapper.Map<Entities.Customer, CustomerResponse>(customer);
            _mapper.Map(person, result);

            return result;
        }
    }
}
