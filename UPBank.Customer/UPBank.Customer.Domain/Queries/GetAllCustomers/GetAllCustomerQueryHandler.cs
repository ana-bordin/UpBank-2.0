using AutoMapper;
using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer;
using UPBank.Customer.Domain.Contracts.Services;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Customer.Domain.Queries.GetAllCustomers
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, GetAllCustomerQueryResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPersonServiceClient _personServiceClient;
        private readonly IMapper _mapper;
        private readonly IDomainNotificationService _domainNotificationService;

        public GetAllCustomerQueryHandler(ICustomerRepository customerRepository, IDomainNotificationService domainNotificationService, IPersonServiceClient personServiceClient, IMapper mapper)
        {
            _personServiceClient = personServiceClient;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _domainNotificationService = domainNotificationService;
        }
        public async Task<GetAllCustomerQueryResponse> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync();

            var response = new GetAllCustomerQueryResponse();

            if (customers != null)
            {
                foreach (var item in customers)
                {
                    if (item.Active)
                    {
                        var person = await _personServiceClient.GetPersonByCPFAsync(item.CPF);
                        var result = _mapper.Map<Entities.Customer, CustomerResponse>(item);
                        _mapper.Map(person, result);

                        response.Customers.Append(result);
                    }
                }
            }
            if (!response.Customers.Any())
            {
                _domainNotificationService.Add("Não há clientes");
                return null;
            }

            return response;
        }
    }
}