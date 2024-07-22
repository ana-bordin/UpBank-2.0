﻿using Microsoft.AspNetCore.Mvc;
using UPBank.Employee.Application.Contracts;
using UPBank.Employee.Application.Models;
using UPBank.Person.Application.Models;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.Person.Contracts;
using UPBank.Utils.Person.Models.DTOs;

namespace UPBank.Employee.API.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPersonService _personService;
        private readonly IAddressService _addressService;

        public EmployeeController(IEmployeeService employeeService, IPersonService personService, IAddressService addressService)
        {
            _employeeService = employeeService;
            _personService = personService;
            _addressService = addressService;
        }

        [HttpPost("api/employees")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeInputModel employeeInputModel)
        {
            var employeeResult = await _employeeService.CreateEmployee(employeeInputModel);
            if (employeeResult.employeeOutputModel == null)
                BadRequest(employeeResult.message);

            return Ok(employeeResult);
        }

        [HttpGet("api/employees/{cpf}")]
        public async Task<IActionResult> GetEmployee(string cpf)
        {
            var employee = await _employeeService.GetEmployeeByCpf(cpf);

            if (employee.employee == null)
                return NotFound();
            if (employee.message != null)
                return BadRequest(employee.message);

            return Ok(employee);
        }

        [HttpPatch("api/employees/{cpf}")]
        public async Task<IActionResult> UpdateEmployee(string cpf, [FromBody] PersonPatchDTO personPatchDTO)
        {
            var ok = await _employeeService.CheckIfExists(cpf);

            if (ok == "funcionário não existe!")
                return NotFound(ok);
            if (ok == "funcionário com restrição!")
                return Forbid(ok);

            var person = await _personService.PatchPerson(cpf, personPatchDTO);

            await _addressService.UpdateAddress(person.AddressId, personPatchDTO.Address);

            if (person == null)
                return BadRequest();

            var employee = await _employeeService.GetEmployeeByCpf(cpf);

            return Ok(employee);
        }

        [HttpDelete("api/employees/{cpf}")]
        public async Task<IActionResult> DeleteEmployee(string cpf)
        {
            var ok = await _employeeService.DeleteEmployeeByCpf(cpf);
            if (!ok.ok)
                return BadRequest();

            return Ok();
        }

        [HttpGet("api/employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        //[HttpPatch("api/employees/setProfileAccount")]
        //public async Task<IActionResult> SetProfileAccount([FromBody] SetProfileDTO setProfileDTO)
        //{
        //    var ok = await _employeeService.SetProfile(setProfileDTO.CPF, setProfileDTO.Manager);
        //    if (!ok)
        //        return BadRequest();

        //    return Ok();
        //}

        //[HttpPatch("api/employees/approveAccountOpening")]
        //public async Task<IActionResult> ApproveAccountOpening([FromBody] ApproveAccountOpeningDTO approveAccountOpeningDTO)
        //{
        //    var ok = await _employeeService.ApproveAccountOpening(approveAccountOpeningDTO.CPF);
        //    if (!ok)
        //        return BadRequest();

        //    return Ok();
        //}

    }
}
