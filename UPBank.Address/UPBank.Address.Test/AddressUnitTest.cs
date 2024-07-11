using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using UPBank.Address.API.Controllers;
using UPBank.Address.Application.Contracts;
using UPBank.Address.Application.Models;
using UPBank.Address.Application.Services;
using UPBank.Address.Domain.Entities;
using UPBank.Address.Infra.Context;

namespace UPBank.Address.Test
{
    public class AddressUnitTest
    {
        #region Testes Positivos

        private readonly AddressController _addressController;
        private readonly Mock<IViaCepService> _viaCepService;
        private readonly Mock<IAddressService> _addressService;
        static readonly Domain.Entities.Address address = new Domain.Entities.Address

        {
            ZipCode = "15997088",
            Street = "Rua 1",
            Neighborhood = "Bairro 1",
            City = "Cidade 1",
            State = "Estado 1"
        };

        static readonly Domain.Entities.Address address2 = new Domain.Entities.Address
        {
            ZipCode = "15997088",
            Street = "Rua 1",
            Neighborhood = "Bairro 1",
            City = "Cidade 1",
            State = "Estado 1"
        };

        static readonly AddressInputModel addressInputModel = new AddressInputModel
        {
            ZipCode = "15997088",
            Number = "123",
            Complement = "Complement"
        };

        static readonly CompleteAddress completeAddress = new CompleteAddress
        {
            Id = id,
            ZipCode = "15997088",
            Number = "123",
            Complement = "Complement"
        };

        static readonly Guid id = Guid.NewGuid();

        static readonly AddressOutputModel addressOutputModel = new AddressOutputModel(addressInputModel, address, id);
        

        public AddressUnitTest()
        {
            _addressService = new Mock<IAddressService>();
            _addressService.Setup(m => m.CreateAddress(It.IsAny<Domain.Entities.Address>())).ReturnsAsync(true);
            _addressService.Setup(m => m.CreateCompleteAddress(It.IsAny<AddressInputModel>())).ReturnsAsync(id);
            _addressService.Setup(m => m.GetCompleteAddressById(It.IsAny<Guid>())).ReturnsAsync(completeAddress);
            _addressService.Setup(m => m.GetAddressByZipCode(It.IsAny<string>())).ReturnsAsync(address2);

            _addressService.Setup(m => m.UpdateAddress(It.IsAny<Guid>(), It.IsAny<AddressInputModel>())).ReturnsAsync(completeAddress);

            _viaCepService = new Mock<IViaCepService>();
            _viaCepService.Setup(m => m.GetAddressInAPI(It.IsAny<string>())).ReturnsAsync(address);

            var upBankApiAddressContext = new Mock<IUpBankApiAddressContext>();
            _addressController = new AddressController(_addressService.Object, _viaCepService.Object);
        }

        [Fact]
        public void CreateAddress_ReturnSucess()
        {
            _addressService.Setup(m => m.GetCompleteAddressById(It.IsAny<Guid>())).ReturnsAsync(completeAddress); 
            _addressService.Setup(m => m.CreateAddress(It.IsAny<Domain.Entities.Address>())).ReturnsAsync(true);
            _addressService.Setup(m => m.CreateCompleteAddress(It.IsAny<AddressInputModel>())).ReturnsAsync(id);

            var result = _addressController.CreateAddress(addressInputModel);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAddress_ReturnSucess()
        {
            _addressService.Setup(m => m.GetAddressByZipCode(It.IsAny<string>())).ReturnsAsync(address2);

            var result = _addressController.GetAddressById(id);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateAddress_ReturnSucess()
        {
            var result = _addressController.UpdateAddress(id, addressInputModel);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteAddress_ReturnSucess()
        {
            var result = _addressController.DeleteAddress(id);
            Assert.IsType<OkResult>(result.Result); 
        }
        #endregion

        #region Testes Negativos

        #endregion
    }
}