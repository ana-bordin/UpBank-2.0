using UPBank.Utils.CommonsFiles.Contracts.Repositories;

namespace UPBank.Employee.Domain.Contracts
{
    public interface IEmployeeRepository : IRepository<Entities.Employee>
    {
        Task<Entities.Employee> SetProfile(string cpf, bool manager);
        Task<Entities.Employee> ApproveAccountOpening(string cpf);
        Task<Entities.Employee> AccountOpeningRequests();
    }
}
