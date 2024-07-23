using UPBank.Employee.Application.Contracts;
using UPBank.Employee.Application.Models;
using UPBank.Employee.Application.Models.DTOs;
using UPBank.Employee.Application.RabbitMQ;
using UPBank.Employee.Domain.Contracts;
using UPBank.Utils.Person.Contracts;

namespace UPBank.Employee.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPersonService _personService;
        private readonly RabbitMQConsumer _rabbitMQService;

        public EmployeeService(IEmployeeRepository employeeRepository, IPersonService personService, RabbitMQConsumer rabbitMQConsumer)
        {
            _employeeRepository = employeeRepository;
            _personService = personService;
            _rabbitMQService = rabbitMQConsumer;
        }

        public async Task<(EmployeeOutputModel employeeOutputModel, string message)> CreateEmployee(EmployeeInputModel employeeInputModel)
        {
            var getEmployee = await GetEmployeeByCpf(employeeInputModel.CPF);
            if (getEmployee.employee != null)
                return (null, "funcionário já existe!");

            var ok = await _personService.CreatePerson(employeeInputModel);

            if (ok.ok)
            {
                Domain.Entities.Employee employeeEntity = new Domain.Entities.Employee
                {
                    CPF = employeeInputModel.CPF,
                    Manager = employeeInputModel.Manager,
                    RecordNumber = Guid.NewGuid()
                };

                var employee = await _employeeRepository.CreateEmployee(employeeEntity);
                return (await CreateEmployeeOutputModel(employee.employee), employee.message);
            }

            return (null, ok.message);
        }

        public async Task<EmployeeOutputModel> CreateEmployeeOutputModel(Domain.Entities.Employee employee)
        {
            if (employee != null)
            {
                var person = await _personService.GetPersonByCpf(employee.CPF);

                var employeeOutputModel = new EmployeeOutputModel
                {
                    CPF = person.person.CPF,
                    Name = person.person.Name,
                    BirthDate = person.person.BirthDate,
                    Address = person.person.Address,
                    Gender = person.person.Gender,
                    Salary = person.person.Salary,
                    Email = person.person.Email,
                    Phone = person.person.Phone,
                    Manager = employee.Manager
                };

                return employeeOutputModel;
            }
            return null;
        }

        public async Task<(bool ok, string message)> DeleteEmployeeByCpf(string cpf)
        {
            return await _employeeRepository.DeleteEmployeeByCpf(cpf);
        }

        public async Task<(IEnumerable<EmployeeOutputModel> employees, string message)> GetAllEmployees()
        {
            var employeesList = await _employeeRepository.GetAllEmployees();
            if (employeesList.message != null)
                return (null, employeesList.message);

            var employeesOutputList = new List<EmployeeOutputModel>();

            foreach (var employee in employeesList.employees)
                employeesOutputList.Add(await CreateEmployeeOutputModel(employee));

            return (employeesOutputList, null);
        }

        public async Task<(EmployeeOutputModel employee, string message)> GetEmployeeByCpf(string cpf)
        {
            var employee = await _employeeRepository.GetEmployeeByCpf(cpf);
            if (employee.employee == null)
                return (null, "funcionário não existe!");
            else
                return (await CreateEmployeeOutputModel(employee.employee), null);
        }

        public async Task<(EmployeeOutputModel employee, string message)> PatchEmployee(string cpf, EmployeePatchDTO employeePatchDTO)
        {
            var getEmployee = await GetEmployeeByCpf(cpf);
            if (getEmployee.employee == null)
                return (null, "funcionário não existe!");

            var person = await _personService.PatchPerson(cpf, employeePatchDTO);

            if (person.person == null)
                return (null, person.message);

            var employee = await _employeeRepository.PatchEmployee(cpf, employeePatchDTO.Manager);
            if (employee.employee == null)
                return (null, employee.message);

            return (await CreateEmployeeOutputModel(employee.employee), null);
        }






        public Task<bool> SetProfile(SetProfileDTO setProfileDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApproveAccountOpening(ApproveAccountOpeningDTO approveAccountOpeningDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AccountOpeningRequests()
        {
            throw new NotImplementedException();
        }
    }
}
