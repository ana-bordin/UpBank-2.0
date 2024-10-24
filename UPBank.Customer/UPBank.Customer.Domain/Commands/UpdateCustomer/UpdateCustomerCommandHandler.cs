using AutoMapper;
using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer;
using UPBank.Customer.Domain.Contracts.Services;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Customer.Domain.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly IPersonServiceClient _personServiceClient;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IDomainNotificationService domainNotificationService, IPersonServiceClient personServiceClient)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _domainNotificationService = domainNotificationService;
            _personServiceClient = personServiceClient;
        }

        public async Task<CustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            return null;
            //var result = await _personServiceClient.UpdatePersonAsync(request.CPF, request);

            //return _mapper.Map<CreatePersonCommandResponse, CustomerResponse>(result);
        }
    }
}
