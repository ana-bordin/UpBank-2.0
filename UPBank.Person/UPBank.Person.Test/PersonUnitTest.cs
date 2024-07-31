using Microsoft.AspNetCore.Mvc;
using Moq;
using UPBank.Address.Application.Models;
using UPBank.Person.API.Controllers;
using UPBank.Person.Application.Contracts;
using UPBank.Person.Application.Services;
using UPBank.Person.Domain.Contracts;
using UPBank.Person.Test.Mocks.Entities;
using UPBank.Utils.Address.Contracts;

namespace UPBank.Person.Test
{
    public class PersonUnitTest
    {
        private readonly PersonController _personController;
        private readonly IPersonService _personService;
        private readonly Mock<IPersonRepository> _personRepository;
        private readonly Mock<IAddressService> _addressService;

        public PersonUnitTest()
        {
            _addressService = new Mock<IAddressService>();
            _personRepository = new Mock<IPersonRepository>();
            _personService = new PersonService(_personRepository.Object, _addressService.Object);
            _personController = new PersonController(_personService);
        }

        #region Testes Positivos

        [Fact]
        public void CreatePerson_ReturnSucess()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((null, null));
            _personRepository.Setup(m => m.CreatePerson(It.IsAny<Domain.Entities.Person>())).ReturnsAsync((PersonMock.Person, null));

            _addressService.Setup(m => m.CreateAddress(It.IsAny<AddressInputModel>())).ReturnsAsync((AddressMock.AddressOutputModel, null));
            _addressService.Setup(m => m.GetCompleteAddressById(It.IsAny<Guid>())).ReturnsAsync((AddressMock.AddressOutputModel, null));

            var result = _personController.CreatePerson(PersonMock.PersonInputModel);
            
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetPerson_ReturnSucess()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((PersonMock.PersonGet, null));

            var result = _personController.GetPerson("96649661007");
            
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void UpdatePerson_ReturnSucess()
        {

            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((PersonMock.Person, ""));
            _personRepository.Setup(m => m.PatchPerson(It.IsAny<string>(), It.IsAny<Domain.Entities.Person>())).ReturnsAsync((PersonMock.Person, null));

            var result = _personController.UpdatePerson("12345678901", PersonMock.PersonPatchDTO);
            
            Assert.IsType<OkObjectResult>(result.Result);
        }

        #endregion

        #region Testes Negativos
        [Fact]
        public void UpdatePerson_ReturnBadRequest()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((PersonMock.PersonUpdate, null));
            _personRepository.Setup(m => m.PatchPerson(It.IsAny<string>(), It.IsAny<Domain.Entities.Person>())).ReturnsAsync((null, "erro"));

            var result = _personController.UpdatePerson("96649661007", PersonMock.PersonPatchDTO);
            
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetPerson_ReturnNotFound()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((null, null));

            var result = _personController.GetPerson("12345678901");
            
            Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Pessoa não encontrada", ((NotFoundObjectResult)result.Result).Value);
        }

        [Fact]
        public void CreatePerson_ReturnBadRequest()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((null, "error"));
            _addressService.Setup(m => m.CreateAddress(It.IsAny<AddressInputModel>())).ReturnsAsync((AddressMock.AddressOutputModel, null));
            _addressService.Setup(m => m.GetCompleteAddressById(It.IsAny<Guid>())).ReturnsAsync((AddressMock.AddressOutputModel, null));

            var result = _personController.CreatePerson(PersonMock.PersonInputModel);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreatePersonWithInvalidDocument_ReturnBadRequest()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((null, null));
            _addressService.Setup(m => m.CreateAddress(It.IsAny<AddressInputModel>())).ReturnsAsync((AddressMock.AddressOutputModel, null));

            var result = _personController.CreatePerson(PersonMock.InvalidPersonCPF);

            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("CPF inválido", ((BadRequestObjectResult)result.Result).Value);
        }

        [Fact]
        public void CreatePersonWithInvalidName_ReturnBadRequest()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((null, null));
            _addressService.Setup(m => m.CreateAddress(It.IsAny<AddressInputModel>())).ReturnsAsync((AddressMock.AddressOutputModel, null));

            var result = _personController.CreatePerson(PersonMock.InvalidPersonName);

            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Nome inválido", ((BadRequestObjectResult)result.Result).Value);
        }

        [Fact]
        public void CreatePersonWithInvalidEmail_ReturnBadRequest()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((null, null));
            _addressService.Setup(m => m.CreateAddress(It.IsAny<AddressInputModel>())).ReturnsAsync((AddressMock.AddressOutputModel, null));

            var result = _personController.CreatePerson(PersonMock.InvalidPersonEmail);
            
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Email inválido", ((BadRequestObjectResult)result.Result).Value);
        }

        [Fact]
        public void CreatePersonWithInvalidPhone_ReturnBadRequest()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((null, null));
            _addressService.Setup(m => m.CreateAddress(It.IsAny<AddressInputModel>())).ReturnsAsync((AddressMock.AddressOutputModel, null));

            var result = _personController.CreatePerson(PersonMock.InvalidPersonPhone);
            
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Telefone inválido", ((BadRequestObjectResult)result.Result).Value);
        }

        [Fact]
        public void CreatePersonWithInvalidSalary_ReturnBadRequest()
        {

            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((null, null));
            _addressService.Setup(m => m.CreateAddress(It.IsAny<AddressInputModel>())).ReturnsAsync((AddressMock.AddressOutputModel, null));

            var result = _personController.CreatePerson(PersonMock.InvalidPersonSalary);
            
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Salário inválido", ((BadRequestObjectResult)result.Result).Value);
        }
        #endregion
    }
}