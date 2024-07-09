﻿using Microsoft.AspNetCore.Mvc;
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

            var result = await _customerService.CreateCustomer(customerInputModel.CPF);

            if (!result)
                return BadRequest();

            var customerResult = new CustomerOutputModel
            {
                CPF = person.CPF,
                Name = person.Name,
                BirthDate = person.BirthDate,
                Gender = person.Gender,
                Salary = person.Salary,
                Email = person.Email,
                Phone = person.Phone,
                Address = add,
                Restriction = false
            };

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
            var person = await _personService.GetPersonByCpf(cpf);
            if (person == null)
                NotFound();

            var address = await _addressService.UpdateAddress(person.AddressId, personPatchDTO.Address);

            var ok = await _personService.PatchPerson(cpf, personPatchDTO);

            if (!ok)
                return NotFound();

            else
            {
                var customer = await _customerService.GetCustomerByCpf(cpf);

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
            var customer = await _customerService.GetCustomerByCpf(cpf);
            if (customer == null)
                return NotFound();

            var ok = await _customerService.DeleteCustomerByCpf(cpf);
            if (!ok)
                return NotFound();

            return Ok();
        }

        [HttpGet("api/customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

    }
}
