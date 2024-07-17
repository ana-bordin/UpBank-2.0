using Microsoft.AspNetCore.Mvc;
using Moq;
using UPBank.Person.API.Controllers;
using UPBank.Person.Application.Contracts;
using UPBank.Person.Application.Models.DTOs;
using UPBank.Person.Application.Services;
using UPBank.Person.Domain.Contracts;

namespace UPBank.Person.Test
{
    public class PersonUnitTest
    {
        private readonly PersonController _personController;
        private readonly IPersonService _personService;
        private readonly Mock<IPersonRepository> _personRepository;

        public PersonUnitTest()
        {
            _personRepository = new Mock<IPersonRepository>();
            _personService = new PersonService(_personRepository.Object);
            _personController = new PersonController(_personService);
        }
        static Domain.Entities.Person person = new Domain.Entities.Person
        {
            CPF = "12345678901",
            Name = "Person Name",
            BirthDate = DateTime.Parse("2000/10/10"),
            AddressId = Guid.NewGuid(),
            Email = "test@gmail.com",
            Gender = 'm',
            Phone = "16991999999",
            Salary = 30000

        };

        static PersonPatchDTO personPatchDTO = new PersonPatchDTO
        {
            Name = "Person Name update",
            Email = ""
        };
        #region Testes Positivos

        [Fact]
        public void CreatePerson_ReturnSucess()
        {
            _personRepository.Setup(m => m.CreatePerson(person)).ReturnsAsync((true, ""));

            var result = _personController.CreatePerson(person);
            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public void GetPerson_ReturnSucess()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((person, ""));

            var result = _personController.GetPerson("12345678901");
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void UpdatePerson_ReturnSucess()
        {

            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((person, ""));
            _personRepository.Setup(m => m.PatchPerson(It.IsAny<string>(), It.IsAny<Domain.Entities.Person>())).ReturnsAsync((person, null));

            var result = _personController.UpdatePerson("12345678901", personPatchDTO);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        #endregion

        #region Testes Negativos
        [Fact]
        public void UpdatePerson_ReturnBadRequest()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((person, ""));
            _personRepository.Setup(m => m.PatchPerson(It.IsAny<string>(), It.IsAny<Domain.Entities.Person>())).ReturnsAsync((null, "erro"));

            var result = _personController.UpdatePerson("12345678901", personPatchDTO);
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
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((null, null));

            var result = _personController.CreatePerson(person);
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Erro ao criar pessoa", ((BadRequestObjectResult)result.Result).Value);
        }

        [Fact]
        public void CreatePersonWithInvalidDocument_ReturnBadRequest()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((null, null));
            var invalidPerson = new Domain.Entities.Person
            {
                CPF = "1234567890",
            };
            // (invalidPerson));
            var result = _personController.CreatePerson(invalidPerson);

            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("CPF inválido", ((BadRequestObjectResult)result.Result).Value);
        }

        [Fact]
        public void CreatePersonWithExistingDocument_ReturnBadRequest()
        {
            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((person, ""));

            var result = _personController.CreatePerson(person);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("CPF já cadastrado", ((OkObjectResult)result.Result).Value);
        }

        [Fact]

        public void CreatePersonWithInvalidName_ReturnBadRequest()
        {
            var invalidPerson = new Domain.Entities.Person
            {
                CPF = "53378006048",
                Name = ""
            };

            _personRepository.Setup(m => m.GetPersonByCpf(It.IsAny<string>())).ReturnsAsync((person, ""));

            var result = _personController.CreatePerson(person);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Nome inválido", ((OkObjectResult)result.Result).Value);
        }

        #endregion
    }
}