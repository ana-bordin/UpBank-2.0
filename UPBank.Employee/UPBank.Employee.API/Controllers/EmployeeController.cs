using Microsoft.AspNetCore.Mvc;
using UPBank.Employee.Application.Contracts;
using UPBank.Employee.Application.Models;
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
            var address = await _addressService.CreateAddress(employeeInputModel.Address);

            var person = new Person.Domain.Entities.Person
            {
                CPF = employeeInputModel.CPF,
                Name = employeeInputModel.Name,
                BirthDate = employeeInputModel.BirthDate,
                Gender = employeeInputModel.Gender,
                Salary = employeeInputModel.Salary,
                Email = employeeInputModel.Email,
                Phone = employeeInputModel.Phone,
                AddressId = address.Id
            };

            await _personService.CreatePerson(person);

            var employeeResult = await _employeeService.CreateEmployee(employeeInputModel.CPF, employeeInputModel.Manager);
            return Ok(employeeResult);
        }

        [HttpGet("api/employees/{cpf}")]
        public async Task<IActionResult> GetEmployee(string cpf)
        {
            var employee = await _employeeService.GetEmployeeByCpf(cpf);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPatch("api/customers/{cpf}")]
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

        [HttpDelete("api/customers/{cpf}")]
        public async Task<IActionResult> DeleteEmployee(string cpf)
        {
            var ok = await _employeeService.DeleteEmployeeByCpf(cpf);
            if (!ok)
                return BadRequest();

            return Ok();
        }

        [HttpGet("api/customers")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees);
        }
    }
}
