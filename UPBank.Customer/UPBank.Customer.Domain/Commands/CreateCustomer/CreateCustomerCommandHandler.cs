using AutoMapper;
using MediatR;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Address;
using UPBank.Customer.Domain.Commands.CreateCustomer.Models.Customer;
using UPBank.Customer.Domain.Contracts.Repositories;
using UPBank.Customer.Domain.Contracts.Services;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Customer.Domain.Entities;
using UPBank.Customer.Domain.RabbitMQ;
using UPBank.Customer.Domain.Services;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Customer.Domain.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
    {
        private readonly IRepository<Person> _personRepository;
        private readonly ICustomerRepository _customerRepository;
        private IMapper _mapper;
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        private readonly IAddressServiceClient _addressServiceClient;

        public CreateCustomerCommandHandler(IRepository<Person> personRepository, IMapper mapper, IDomainNotificationService domainNotificationService, ICustomerRepository customerRepository, RabbitMQPublisher rabbitMQPublisher, IAddressServiceClient addressServiceClient)
        {
            _rabbitMQPublisher = rabbitMQPublisher;
            _personRepository = personRepository;
            _mapper = mapper;
            _domainNotificationService = domainNotificationService;
            _customerRepository = customerRepository;
            _addressServiceClient = addressServiceClient;
        }

        async Task<CreateCustomerCommandResponse> IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>.Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var listCustomers = new List<Entities.Customer>();
            var createCustomerCommandResponseList = new CreateCustomerCommandResponse();
            createCustomerCommandResponseList.Customers = new List<CustomerResponse>();
            try
            {
                foreach (var customerRequest in request.Customers)
                {
                    var addressResponseData = await _addressServiceClient.CreateAddress(_mapper.Map<AddressRequest, AddressRequestData>(customerRequest.Address));

                    var addressResponse = _mapper.Map<AddressResponseData, AddressResponse>(addressResponseData);

                    var person = _mapper.Map<CustomerRequest, Person>(customerRequest);
                    person.AddressId = addressResponse.Id;

                    var personResponse = await _personRepository.AddAsync(person);

                    var customer = _mapper.Map<CustomerRequest, Entities.Customer>(customerRequest);

                    customer = await _customerRepository.AddAsync(customer);

                    listCustomers.Add(customer);

                    var customerResponse = _mapper.Map<Entities.Customer, CustomerResponse>(customer);
                    customerResponse.Address = addressResponse;
                    customerResponse = _mapper.Map<Person, CustomerResponse>(personResponse);
                    
                    createCustomerCommandResponseList.Customers.Add(customerResponse);
                }

                _rabbitMQPublisher.Publish(listCustomers);

                return createCustomerCommandResponseList;

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}