using Microsoft.AspNetCore.Mvc;
using UPBank.Customer.Application.Contracts;
using UPBank.Person.Application.Models;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.Person.Contracts;
using UPBank.Utils.Person.Models.DTOs;

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
        public async Task<IActionResult> CreateCustomer([FromBody] PersonInputModel personInputModel)
        {
            var person = await _personService.CreatePerson(personInputModel);

            if (!person.ok)
                return BadRequest(person.message);

            var customer = await _customerService.CreateCustomer(personInputModel.CPF);

            if (customer.customerOutputModel == null)
                return BadRequest(customer.message);

            return Ok(customer.customerOutputModel);
        }

        [HttpGet("api/customers/{cpf}")]
        public async Task<IActionResult> GetCustomer(string cpf)
        {
            var customer = await _customerService.GetCustomerByCpf(cpf);

            if (customer.customerOutputModel == null)
                return NotFound();

            return Ok(customer);
        }









        [HttpPatch("api/customers/{cpf}")]
        public async Task<IActionResult> UpdateCustomer(string cpf, [FromBody] PersonPatchDTO personPatchDTO)
        {
            var ok = await _customerService.CheckIfExists(cpf);

            if (ok == "cliente não existe!")
                return NotFound(ok);
            if (ok == "cliente com restrição!")
                return Forbid(ok);

            var person = await _personService.PatchPerson(cpf, personPatchDTO);

            if (person.person == null)
                return BadRequest();

            var customer = await _customerService.GetCustomerByCpf(cpf);

            return Ok(customer);
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

        [HttpPatch("api/customers/patchRestriction/{cpf}")]
        public async Task<IActionResult> CustomerPatchRestriction(string cpf)
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
