using UPBank.Customer.Application.Contracts;
using UPBank.Customer.Domain.Contracts;
using UPBank.Customer.Application.Models;
using UPBank.Utils.Address.Contracts;
using UPBank.Customer.Application.RabbitMQ;

namespace UPBank.Customer.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressService _addressService;
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public CustomerService(ICustomerRepository customerRepository, IAddressService addressService, RabbitMQPublisher rabbitMQPublisher)
        {
            _customerRepository = customerRepository;
            _addressService = addressService;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<Domain.Entities.Customer> CreateCustomer(string cpf)
        {
            var customer = await _customerRepository.CreateCustomer(cpf);
            _rabbitMQPublisher.Publish(customer);
            return customer;
        }
        
        public async Task<Domain.Entities.Customer> GetCustomerByCpf(string cpf)
        {
            return await _customerRepository.GetCustomerByCpf(cpf);
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
            {
                customersOutputList.Add(new CustomerOutputModel
                {
                    CPF = customer.CPF,
                    Name = customer.Name,
                    BirthDate = customer.BirthDate,
                    Address = await _addressService.GetCompleteAddressById(customer.AddressId),
                    Gender = customer.Gender,
                    Salary = customer.Salary,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Restriction = customer.Restriction
                });
            }

            return customersOutputList;
        }
    }
}
