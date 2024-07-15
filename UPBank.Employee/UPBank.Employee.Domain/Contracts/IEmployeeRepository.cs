namespace UPBank.Employee.Domain.Contracts
{
    public interface IEmployeeRepository
    {
        Task<Entities.Employee> CreateEmployee(string cpf, bool manager);
        Task<Entities.Employee> GetEmployeeByCpf(string cpf);
        Task<bool> DeleteEmployeeByCpf(string cpf);
        Task<IEnumerable<Entities.Employee>> GetAllEmployees();



        Task<Entities.Employee> SetProfile(string cpf, bool manager);
        Task<Entities.Employee> ApproveAccountOpening(string cpf);

    }
}
