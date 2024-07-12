using Microsoft.AspNetCore.Mvc;
using UPBank.Customer.Application.Contracts;
using UPBank.Customer.Application.Models;
using UPBank.Customer.Application.Models.DTOs;
using UPBank.Customer.Domain.Entities;
using UPBank.Utils.Address.Contracts;

namespace UPBank.Customer.API.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        //private readonly ILogger<CustomerController> _logger;
        private readonly IPersonService _personService;
        private readonly IAddressService _addressService;

        public CustomerController(ICustomerService customerService, IPersonService personService, IAddressService addressService)
        {
            _customerService = customerService;
            _personService = personService;
            _addressService = addressService;
        }

        [HttpPost("api/customers")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerInputModel customerInputModel)
        {
            var add = await _addressService.CreateAddress(customerInputModel.Address);

            var person = new Person
            {
                CPF = customerInputModel.CPF,
                Name = customerInputModel.Name,
                BirthDate = customerInputModel.BirthDate,
                Gender = customerInputModel.Gender,
                Salary = customerInputModel.Salary,
                Email = customerInputModel.Email,
                Phone = customerInputModel.Phone,
                AddressId = add.Id
            };

            if (_personService.CheckIfExist(customerInputModel.CPF).Result)
                await _personService.CreatePerson(person);

            var customerResult = await _customerService.CreateCustomer(customerInputModel.CPF);

            if (customerResult == null)
                return BadRequest();
           
            return Ok(customerResult);
        }

        [HttpGet("api/customers/{cpf}")]
        public async Task<IActionResult> GetCustomer(string cpf)
        {
            var customer = await _customerService.GetCustomerByCpf(cpf);

            if (customer == null)
                return NotFound();

            var person = await _personService.GetPersonByCpf(cpf);
            
            var customerOutputModel = new CustomerOutputModel
            {
                CPF = person.CPF,
                Name = person.Name,
                BirthDate = person.BirthDate,
                Gender = person.Gender,
                Salary = person.Salary,
                Email = person.Email,
                Phone = person.Phone,
                Address = await _addressService.GetCompleteAddressById(customer.AddressId),
                Restriction = customer.Restriction,
            };

            return Ok(customerOutputModel);
        }

        [HttpPatch("api/customers/{cpf}")]
        public async Task<IActionResult> UpdateCustomer(string cpf, [FromBody] PersonPatchDTO personPatchDTO)
        {
            var customer = await _customerService.GetCustomerByCpf(cpf);

            if (customer == null)
                return NotFound();

            var person = await _personService.GetPersonByCpf(cpf);

            await _addressService.UpdateAddress(person.AddressId, personPatchDTO.Address);

            var ok = await _personService.PatchPerson(cpf, personPatchDTO);

            if (!ok)
                return NotFound();

            else
            {    
                var customerOutputModel = new CustomerOutputModel
                {
                    CPF = person.CPF,
                    Name = person.Name,
                    BirthDate = person.BirthDate,
                    Gender = person.Gender,
                    Salary = person.Salary,
                    Email = person.Email,
                    Phone = person.Phone,
                    Address = await _addressService.GetCompleteAddressById(customer.AddressId),
                    Restriction = customer.Restriction,
                };

                return Ok(customerOutputModel);
            }
        }

        [HttpDelete("api/customers/{cpf}")]
        public async Task<IActionResult> DeleteCustomer(string cpf)
        {
            var ok = await _customerService.DeleteCustomerByCpf(cpf);
            if (!ok)
                return BadRequest();

            return Ok();
        }

        [HttpGet("api/customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        
        
        
        
        [HttpPatch("api/customers/restriction/{cpf}")]
        public async Task<IActionResult> CustomersRestriction(string cpf)
        {
            var customer = await _customerService.CustomerRestriction(cpf);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }
















        [HttpGet("api/customers/restriction")]
        public async Task<IActionResult> GetCustomerWithRestriction()
        {
            var customers = await _customerService.GetCustomersWithRestriction();
            if (customers == null)
                return NotFound();

            return Ok(customers);
        }

        [HttpPost("api/customers/accountOpening")]
        public async Task<IActionResult> AccountOpening(List<string> cpfs)
        {
            foreach (var cpf in cpfs)
            {
                var customer = await _customerService.GetCustomerByCpf(cpf);
                if (customer == null)
                    return NotFound();
            }   

            var account = await _customerService.AccountOpening(cpfs);

            if (account == null)
                return BadRequest();

            return Ok();
        }


    }
}
