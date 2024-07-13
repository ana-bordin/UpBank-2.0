using UPBank.Customer.Application.Contracts;
using UPBank.Customer.Domain.Contracts;
using UPBank.Customer.Application.Models;
using UPBank.Utils.Address.Contracts;
using UPBank.Customer.Application.RabbitMQ;
using UPBank.Utils.Person.Contracts;

namespace UPBank.Customer.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressService _addressService;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        private readonly IPersonService _personService;

        public CustomerService(ICustomerRepository customerRepository, IAddressService addressService, RabbitMQPublisher rabbitMQPublisher, IPersonService personService)
        {
            _customerRepository = customerRepository;
            _addressService = addressService;
            _rabbitMQPublisher = rabbitMQPublisher;
            _personService = personService;
        }

        public async Task<CustomerOutputModel> CreateCustomer(string cpf)
        {
            var customer = await _customerRepository.CreateCustomer(cpf);
            if (customer != null)
                _rabbitMQPublisher.Publish(customer);
            return await CreateCustomerOutputModel(customer);

        }

        public async Task<bool> CheckIfExists(string cpf)
        {
            var customer = await _customerRepository.GetCustomerByCpf(cpf);
            if (customer == null)
                return false;
            return true;
        }

        public async Task<CustomerOutputModel> GetCustomerByCpf(string cpf)
        {
            var customer = await _customerRepository.GetCustomerByCpf(cpf);
            return await CreateCustomerOutputModel(customer);
        }

        public async Task<bool> DeleteCustomerByCpf(string cpf)
        {
            return await _customerRepository.DeleteCustomerByCpf(cpf);
        }

        public async Task<IEnumerable<CustomerOutputModel>> GetAllCustomers()
        {
            var customersList = await _customerRepository.GetAllCustomers();
            var customersOutputList = new List<CustomerOutputModel>();
            foreach (var customer in customersList)
                customersOutputList.Add(CreateCustomerOutputModel(customer).Result);

            return customersOutputList;
        }

        public async Task<CustomerOutputModel> CreateCustomerOutputModel(Domain.Entities.Customer customer)
        {
            if (customer != null)
            {
                var person = await _personService.GetPersonByCpf(customer.CPF);
                var address = await _addressService.GetCompleteAddressById(person.AddressId);
                return new CustomerOutputModel
                {
                    CPF = person.CPF,
                    Name = person.Name,
                    BirthDate = person.BirthDate,
                    Address = address,
                    Gender = person.Gender,
                    Salary = person.Salary,
                    Email = person.Email,
                    Phone = person.Phone,
                    Restriction = customer.Restriction
                };
            }

            return null;
        }













        public async Task<CustomerOutputModel> CustomerRestriction(string cpf)
        {
            var customer = await _customerRepository.CustomerRestriction(cpf);
            if (customer == null)
                return null;

            else
            {
                return null;
            }

        }

        public Task<IEnumerable<CustomerOutputModel>> GetCustomersWithRestriction()
        {
            var allCustomers = _customerRepository.GetCustomersWithRestriction();
            return null;
        }

        public Task<bool> AccountOpening(List<string> cpfs)
        {
            return _customerRepository.AccountOpening(cpfs);

        }
    }
}
