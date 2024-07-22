using UPBank.Employee.Application.Contracts;
using UPBank.Employee.Application.Models;
using UPBank.Employee.Application.RabbitMQ;
using UPBank.Employee.Infra.Repositories;
using UPBank.Person.Application.Models;
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
            if (employee.employee == null)
                return "funcionário não existe!";
            return "ok";
        }

        public async Task<(EmployeeOutputModel employeeOutputModel, string message)> CreateEmployee(EmployeeInputModel employeeInputModel)
        {
            var getEmployee = GetEmployeeByCpf(employeeInputModel.CPF);
            if (getEmployee != null)
                return (null, "funcionário já existe!");

            var personInputModel = new PersonInputModel
            {
                CPF = employeeInputModel.CPF,
                Name = employeeInputModel.Name,
                BirthDate = employeeInputModel.BirthDate,
                Gender = employeeInputModel.Gender,
                Salary = employeeInputModel.Salary,
                Email = employeeInputModel.Email,
                Phone = employeeInputModel.Phone,
                Address = employeeInputModel.Address
            };

            var ok = await _personService.CreatePerson(personInputModel);

            if (ok.ok)
            {
                var employee = await _employeeRepository.CreateEmployee(employeeInputModel.CPF, employeeInputModel.Manager);
                return (await CreateEmployeeOutputModel(employee.employee), employee.message);
            }

            return (null, ok.message);
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
