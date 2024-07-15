using UPBank.Employee.Application.Contracts;
using UPBank.Employee.Application.Models;
using UPBank.Employee.Application.RabbitMQ;
using UPBank.Employee.Infra.Repositories;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.Person.Contracts;

namespace UPBank.Employee.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly IAddressService _addressService;
        private readonly IPersonService _personService;
        private readonly RabbitMQConsumer _rabbitMQService;

        public EmployeeService(EmployeeRepository employeeRepository, IAddressService addressService, IPersonService personService, RabbitMQConsumer rabbitMQConsumer)
        {
            _employeeRepository = employeeRepository;
            _addressService = addressService;
            _personService = personService;
            _rabbitMQService = rabbitMQConsumer;
        }

        public async Task<string> CheckIfExists(string cpf)
        {
            var employee = await _employeeRepository.GetEmployeeByCpf(cpf);
            if (employee == null)
                return "funcionário não existe!";
            return "ok";
        }

        public async Task<EmployeeOutputModel> CreateEmployee(string cpf, bool manager)
        {
            var employee = await _employeeRepository.CreateEmployee(cpf, manager);
            return await CreateEmployeeOutputModel(employee);
        }

        public async Task<EmployeeOutputModel> CreateEmployeeOutputModel(Domain.Entities.Employee employee)
        {
            if (employee != null)
            {
                var person = await _personService.GetPersonByCpf(employee.CPF);
                var address = await _addressService.GetCompleteAddressById(person.AddressId);

                var employeeOutputModel = new EmployeeOutputModel
                {
                    CPF = person.CPF,
                    Name = person.Name,
                    BirthDate = person.BirthDate,
                    Address = address,
                    Gender = person.Gender,
                    Salary = person.Salary,
                    Email = person.Email,
                    Phone = person.Phone,
                    Manager = employee.Manager
                };
            }
            return null;
        }

        public async Task<bool> DeleteEmployeeByCpf(string cpf)
        {
            return await _employeeRepository.DeleteEmployeeByCpf(cpf);
        }

        public async Task<IEnumerable<EmployeeOutputModel>> GetAllEmployees()
        {
            var employeesList = await _employeeRepository.GetAllEmployees();
            var employeesOutputList = new List<EmployeeOutputModel>();
            
            foreach (var employee in employeesList)
               employeesOutputList.Add(await CreateEmployeeOutputModel(employee));
            
            return employeesOutputList;
        }

        public async Task<EmployeeOutputModel> GetEmployeeByCpf(string cpf)
        {
            var employee = await _employeeRepository.GetEmployeeByCpf(cpf);
            return await CreateEmployeeOutputModel(employee);
        }




        public Task<bool> SetProfile(string cpf, bool manager)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ApproveAccountOpening(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}
