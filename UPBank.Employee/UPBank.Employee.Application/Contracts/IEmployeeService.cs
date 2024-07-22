using UPBank.Employee.Application.Models;

namespace UPBank.Employee.Application.Contracts
{
    public interface IEmployeeService
    {
        Task<(EmployeeOutputModel employeeOutputModel, string message)> CreateEmployee(EmployeeInputModel employeeInputModel);
        Task<(IEnumerable<EmployeeOutputModel> employees, string message)> GetAllEmployees();
        Task<(EmployeeOutputModel employee, string message)> GetEmployeeByCpf(string cpf);
        Task<(bool ok, string message)> DeleteEmployeeByCpf(string cpf);
        Task<EmployeeOutputModel> CreateEmployeeOutputModel(Domain.Entities.Employee employee);
        Task<string> CheckIfExists(string cpf);


        Task<bool> SetProfile(string cpf, bool manager);
        Task<bool> ApproveAccountOpening(string cpf);

    }
}
