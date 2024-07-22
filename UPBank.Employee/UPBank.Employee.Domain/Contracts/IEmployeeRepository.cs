namespace UPBank.Employee.Domain.Contracts
{
    public interface IEmployeeRepository
    {
        Task<(Entities.Employee employee, string message)> CreateEmployee(string cpf, bool manager);
        Task<(Entities.Employee employee, string message)> GetEmployeeByCpf(string cpf);
        Task<(bool ok, string message)> DeleteEmployeeByCpf(string cpf);
        Task<(IEnumerable<Entities.Employee> employees, string message)> GetAllEmployees();
        Task<(Entities.Employee employee, string message)> PatchEmployee(string cpf, bool manager);


        Task<Entities.Employee> SetProfile(string cpf, bool manager);
        Task<Entities.Employee> ApproveAccountOpening(string cpf);

    }
}
