using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPBank.Employee.Domain.Commands.CreateEmployee;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Employee.API.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IDomainNotificationService _domainNotificationService;
        private readonly IMediator _mediator;

        public EmployeeController(IDomainNotificationService domainNotificationService, IMediator mediator)
        {
            _domainNotificationService = domainNotificationService;
            _mediator = mediator;           
        }

        [HttpPost("api/employees")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand createEmployeeCommand, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(createEmployeeCommand, cancellationToken);
            if (_domainNotificationService.HasNotification)
                return BadRequest(_domainNotificationService.Get());

            return Ok(response);
        }

        //[HttpGet("api/employees/{cpf}")]
        //public async Task<IActionResult> GetEmployee(string cpf, CancellationToken cancellationToken)
        //{
        //    var employee = await _mediator.Send(new GetEmployeeByCpfQuery(cpf), cancellationToken);

        //    if (_domainNotificationService.HasNotification)
        //        return BadRequest(_domainNotificationService.Get());

        //    return Ok(employee);
        //}

        ////[HttpPatch("api/employees/{cpf}")]
        ////public async Task<IActionResult> UpdateEmployee(string cpf, [FromBody] EmployeePatchDTO employeePatchDTO)
        ////{
        ////    var employee = await _employeeService.PatchEmployee(cpf, employeePatchDTO);
        ////    if (employee.employee == null)
        ////        return BadRequest(employee.message);

        ////    return Ok(employee);
        ////}

        //[HttpDelete("api/employees/{cpf}")]
        //public async Task<IActionResult> DeleteEmployee(string cpf)
        //{
        //    var ok = await _employeeService.DeleteEmployeeByCpf(cpf);
        //    if (!ok.ok)
        //        return BadRequest();

        //    return Ok();
        //}

        //[HttpGet("api/employees")]
        //public async Task<IActionResult> GetAllEmployees()
        //{
        //    var employees = await _employeeService.GetAllEmployees();
        //    return Ok(employees);
        //}




        //[HttpPatch("api/employees/setProfileAccount")]
        //public async Task<IActionResult> SetProfileAccount([FromBody] SetProfileDTO setProfileDTO)
        //{
        //    var ok = await _employeeService.SetProfile(setProfileDTO);

        //    if (!ok)
        //        return BadRequest();

        //    return Ok();
        //}

        //[HttpPatch("api/employees/accountOpeningRequests")]
        //public async Task<IActionResult> AccountOpeningRequests()
        //{
        //    var accountOpeningRequests = await _customerService.GetAccountOpeningRequests();

        //    return Ok(accountOpeningRequests);
        //}

        //[HttpPatch("api/employees/approveAccountOpening")]
        //public async Task<IActionResult> ApproveAccountOpening([FromBody] ApproveAccountOpeningDTO approveAccountOpeningDTO)
        //{
        //    var ok = await _employeeService.ApproveAccountOpening(approveAccountOpeningDTO);
        //    if (!ok)
        //        return BadRequest();

        //    return Ok();
        //}

    }
}
