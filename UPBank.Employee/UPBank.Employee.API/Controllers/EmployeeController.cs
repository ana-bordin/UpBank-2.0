using Microsoft.AspNetCore.Mvc;
using UPBank.Employee.Application.Contracts;
using UPBank.Employee.Application.Models;
using UPBank.Employee.Application.Models.DTOs;

namespace UPBank.Employee.API.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("api/employees")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeInputModel employeeInputModel)
        {
            var employeeResult = await _employeeService.CreateEmployee(employeeInputModel);
            if (employeeResult.employeeOutputModel == null)
                return BadRequest(employeeResult.message);

            return Ok(employeeResult.employeeOutputModel);
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
        public async Task<IActionResult> UpdateEmployee(string cpf, [FromBody] EmployeePatchDTO employeePatchDTO)
        {
            var employee = await _employeeService.PatchEmployee(cpf, employeePatchDTO);
            if (employee.employee == null)
                return BadRequest(employee.message);

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

       
        
        
        [HttpPatch("api/employees/setProfileAccount")]
        public async Task<IActionResult> SetProfileAccount([FromBody] SetProfileDTO setProfileDTO)
        {
            var ok = await _employeeService.SetProfile(setProfileDTO);
            
            if (!ok)
                return BadRequest();

            return Ok();
        }

        //[HttpPatch("api/employees/accountOpeningRequests")]
        //public async Task<IActionResult> AccountOpeningRequests()
        //{
        //    var accountOpeningRequests = await _customerService.GetAccountOpeningRequests();
            
        //    return Ok(accountOpeningRequests);
        //}

        [HttpPatch("api/employees/approveAccountOpening")]
        public async Task<IActionResult> ApproveAccountOpening([FromBody] ApproveAccountOpeningDTO approveAccountOpeningDTO)
        {
            var ok = await _employeeService.ApproveAccountOpening(approveAccountOpeningDTO);
            if (!ok)
                return BadRequest();

            return Ok();
        }

    }
}
