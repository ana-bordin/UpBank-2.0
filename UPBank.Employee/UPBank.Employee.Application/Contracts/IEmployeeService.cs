using UPBank.Employee.Application.Models;

namespace UPBank.Employee.Application.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeOutputModel> CreateEmployee(string cpf, bool manager);
        Task<IEnumerable<EmployeeOutputModel>> GetAllEmployees();
        Task<EmployeeOutputModel> GetEmployeeByCpf(string cpf);
        Task<bool> DeleteEmployeeByCpf(string cpf);
        Task<EmployeeOutputModel> CreateEmployeeOutputModel(Domain.Entities.Employee employee);
        Task<string> CheckIfExists(string cpf);


        Task<bool> SetProfile(string cpf, bool manager);
        Task<bool> ApproveAccountOpening(string cpf);

    }
}
