using UPBank.Customer.Application.Contracts;
using UPBank.Customer.Application.Models;
using UPBank.Customer.Application.RabbitMQ;
using UPBank.Customer.Domain.Contracts;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.Person.Contracts;
using UPBank.Utils.Person.Models.DTOs;

namespace UPBank.Customer.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        private readonly IPersonService _personService;

        public CustomerService(ICustomerRepository customerRepository, RabbitMQPublisher rabbitMQPublisher, IPersonService personService)
        {
            _customerRepository = customerRepository;
            _rabbitMQPublisher = rabbitMQPublisher;
            _personService = personService;
        }

        public async Task<(CustomerOutputModel customerOutputModel, string message)> CreateCustomer(string cpf)
        {
            var customer = await _customerRepository.CreateCustomer(cpf);

            if (customer.customer != null)
                return (await CreateCustomerOutputModel(customer.customer), null);
            else
                return (null, customer.message);
        }

        public async Task<(CustomerOutputModel customerOutputModel, string message)> GetCustomerByCpf(string cpf)
        {
            var customer = await _customerRepository.GetCustomerByCpf(cpf);

            if (customer.customer != null)
                return (await CreateCustomerOutputModel(customer.customer), null);
            else
                return (null, customer.message);
        }

        public async Task<(bool ok, string message)> DeleteCustomerByCpf(string cpf)
        {
            return await _customerRepository.DeleteCustomerByCpf(cpf);
        }

        public async Task<(IEnumerable<CustomerOutputModel> customers, string message)> GetAllCustomers()
        {
            var customersList = await _customerRepository.GetAllCustomers();
            var customersOutputList = new List<CustomerOutputModel>();
            foreach (var customer in customersList.customers)
                customersOutputList.Add(CreateCustomerOutputModel(customer).Result);

            return (customersOutputList, null);
        }

        public async Task<(CustomerOutputModel customerOutputodel, string message)> CustomerPatchRestriction(string cpf)
        {
            var customer = await _customerRepository.CustomerPatchRestriction(cpf);

            if (customer.customer == null)
                return (null, customer.message);
            else
                return (await CreateCustomerOutputModel(customer.customer), null);
        }

        public async Task<(IEnumerable<CustomerOutputModel> customers, string message)> GetAllCustomersWithRestriction()
        {
            var allCustomers = await _customerRepository.GetAllCustomersWithRestriction();

            if (allCustomers.customers == null)
                return (null, allCustomers.message);

            var customersOutputList = new List<CustomerOutputModel>();
            foreach (var customer in allCustomers.customers)
                customersOutputList.Add(await CreateCustomerOutputModel(customer));

            return (customersOutputList, null);

        }

        public async Task<(bool ok, string message)> AccountOpening(List<string> cpfs)
        {
            List<CustomerOutputModel> customersList = new List<CustomerOutputModel>();
            int majority = 0;
            foreach (var cpf in cpfs)
            {
                var customer = GetCustomerByCpf(cpf).Result;
                if (customer.customerOutputModel == null)
                    return (false, "cliente não existe!");
                if (customer.customerOutputModel.Restriction)
                    return (false, "cliente com restrição!");
                if (customer.customerOutputModel.BirthDate < DateTime.Now.AddYears(-18))
                    majority++;

                customersList.Add(customer.customerOutputModel);
            }
            if (majority == 0)
                return (false, "nenhum cliente é maior de idade para abrir a conta!");

            _rabbitMQPublisher.Publish(customersList);

            return await _customerRepository.AccountOpening(cpfs);
        }

        public async Task<(CustomerOutputModel customerOutputModel, string message)> UpdateCustomer(string cpf, PersonPatchDTO personPatchDTO)
        {
            var customer = await _customerRepository.GetCustomerByCpf(cpf);
            if (customer.customer == null)
                return (null, "cliente não existe!");
            if (customer.customer.Restriction)
                return (null, "cliente com restrição!");

            var personResult = await _personService.PatchPerson(cpf, personPatchDTO);
            if (personResult.person == null)
                return (null, personResult.message);

            return (await CreateCustomerOutputModel(customer.customer), null);
        }

        public async Task<CustomerOutputModel> CreateCustomerOutputModel(Domain.Entities.Customer customer)
        {
            if (customer != null)
            {
                var person = await _personService.GetPersonByCpf(customer.CPF);

                return new CustomerOutputModel
                {
                    CPF = person.person.CPF,
                    Name = person.person.Name,
                    BirthDate = person.person.BirthDate,
                    Address = person.person.Address,
                    Gender = person.person.Gender,
                    Salary = person.person.Salary,
                    Email = person.person.Email,
                    Phone = person.person.Phone,
                    Restriction = customer.Restriction
                };
            }

            return null;
        }
    }
}
