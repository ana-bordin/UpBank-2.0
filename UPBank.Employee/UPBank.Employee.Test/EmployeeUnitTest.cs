using Microsoft.AspNetCore.Mvc;
using Moq;
using UPBank.Employee.API.Controllers;
using UPBank.Employee.Application.Contracts;
using UPBank.Employee.Application.Models;
using UPBank.Employee.Application.Services;
using UPBank.Employee.Domain.Contracts;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.Person.Contracts;

namespace UPBank.Employee.Test
{
    public class EmployeeUnitTest
    {
        private readonly EmployeeController _employeeController;
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly Mock<IPersonService> _personService;
        private readonly Mock<IAddressService> _addressService;

        public EmployeeUnitTest()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _personService = new Mock<IPersonService>();
            _employeeService = new EmployeeService(_employeeRepository.Object, _personService.Object, null);
            _employeeController = new EmployeeController(_employeeService, _personService.Object, _addressService.Object);

        }

        #region Testes Positivos

        [Fact]
        public void CreateEmployee_ReturnSucess()
        {
            _employeeRepository.Setup(m => m.GetEmployeeByCpf(It.IsAny<string>())).ReturnsAsync((null, null));
            _personService.Setup(m => m.CreatePerson(It.IsAny<EmployeeInputModel>())).ReturnsAsync((true, null));
            _employeeRepository.Setup(m => m.CreateEmployee(It.IsAny<string>(), It.IsAny<bool>())).ReturnsAsync((EmployeeMock.Employee, null));

            var result = _employeeController.CreateEmployee(EmployeeMock.EmployeeInputModel);

            Assert.IsType<OkObjectResult>(result.Result);

        }

        [Fact]
        public void GetEmployee_ReturnSucess()
        {
            _employeeRepository.Setup(m => m.GetEmployeeByCpf(It.IsAny<string>())).ReturnsAsync((EmployeeMock.Employee, null));

            var result = _employeeController.GetEmployee("12345678901");

            Assert.IsType<OkObjectResult>(result.Result);

        }

        [Fact]
        public void UpdateEmployee_ReturnSucess()
        {
            _employeeRepository.Setup(m => m.GetEmployeeByCpf(It.IsAny<string>())).ReturnsAsync((EmployeeMock.Employee, null));
            _employeeRepository.Setup(m => m.PatchEmployee(It.IsAny<string>(), It.IsAny<Domain.Entities.Employee>())).ReturnsAsync((EmployeeMock.Employee, null));

            var result = _employeeController.UpdateEmployee("12345678901", EmployeeMock.EmployeePatchDTO);

            Assert.IsType<OkObjectResult>(result.Result);

        }

        #endregion

        #region Testes Negativos
        [Fact]
        public void UpdateEmployee_ReturnBadRequest()
        {
            _employeeRepository.Setup(m => m.GetEmployeeByCpf(It.IsAny<string>())).ReturnsAsync((null, "error"));

            var result = _employeeController.UpdateEmployee("12345678901", EmployeeMock.EmployeePatchDTO);

        }

        [Fact]
        public void GetEmployee_ReturnNotFound()
        {
            _employeeRepository.Setup(m => m.GetEmployeeByCpf(It.IsAny<string>())).ReturnsAsync((null, "error"));

            var result = _employeeController.GetEmployee("12345678901");

            Assert.IsType<NotFoundObjectResult>(result.Result);

        }

        [Fact]
        public void CreateEmployee_ReturnBadRequest()
        {
            _employeeRepository.Setup(m => m.GetEmployeeByCpf(It.IsAny<string>())).ReturnsAsync((EmployeeMock.Employee, null));

            var result = _employeeController.CreateEmployee(EmployeeMock.EmployeeInputModel);

            Assert.IsType<BadRequestObjectResult>(result.Result);

        }
        #endregion
    }
}