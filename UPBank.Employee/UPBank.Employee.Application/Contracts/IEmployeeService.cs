using UPBank.Employee.Application.Models;
using UPBank.Employee.Application.Models.DTOs;

namespace UPBank.Employee.Application.Contracts
{
    public interface IEmployeeService
    {
        Task<(EmployeeOutputModel employeeOutputModel, string message)> CreateEmployee(EmployeeInputModel employeeInputModel);
        Task<(IEnumerable<EmployeeOutputModel> employees, string message)> GetAllEmployees();
        Task<(EmployeeOutputModel employee, string message)> GetEmployeeByCpf(string cpf);
        Task<(bool ok, string message)> DeleteEmployeeByCpf(string cpf);
        Task<EmployeeOutputModel> CreateEmployeeOutputModel(Domain.Entities.Employee employee);
        Task<(EmployeeOutputModel employee, string message)> PatchEmployee(string cpf, EmployeePatchDTO employeePatchDTO);


        Task<bool> SetProfile(SetProfileDTO setProfileDTO);
        Task<bool> ApproveAccountOpening(ApproveAccountOpeningDTO approveAccountOpeningDTO);
        Task<bool> AccountOpeningRequests();

    }
}
