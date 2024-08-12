using MediatR;
using Moq;
using UPBank.Address.API.Controllers;
using UPBank.Address.Domain.Contracts;
using UPBank.Address.Domain.Entities;
using UPBank.Utils.CommonsFiles;
using UPBank.Utils.CommonsFiles.Contracts;

namespace UPBank.Address.Test
{
    public class AddressUnitTest
    {
        private readonly AddressController _addressController;
        private readonly Mock<IViaCepService> _viaCepService;
        private readonly Mock<IAddressRepository> _addressRepository;
        private readonly Mock<IRepository<CompleteAddress>> _completeAddressRepository;
        private readonly Mock<IDomainNotificationService> _domainNotificationService;
        private readonly IMediator _mediator;

        public AddressUnitTest()
        {
            _mediator = new Mock<IMediator>().Object;
            _viaCepService = new Mock<IViaCepService>();
            _addressRepository = new Mock<IAddressRepository>();
            _completeAddressRepository = new Mock<IRepository<CompleteAddress>>();
            //_addressController = new AddressController(_mediator);
            _domainNotificationService = new Mock<IDomainNotificationService>();
        }
        
        #region Testes Positivos
        //[Fact]
        //public void CreateAddress_ReturnSucess()
        //{
        //    _viaCepService.Setup(m => m.GetAddressByZipCode(It.IsAny<string>())).ReturnsAsync(Mocks.Entities.AddressMock.Address);
        //    _addressRepository.Setup(m => m.AddAsync(It.IsAny<Domain.Entities.Address>())).ReturnsAsync((Mocks.Entities.AddressMock.Address, null));
        //    _completeAddressRepository.Setup(m => m.AddAsync(It.IsAny<CompleteAddress>())).ReturnsAsync((true, null));
        //    _addressRepository.Setup(m => m.GetOneAsync(It.IsAny<string>())).ReturnsAsync((Mocks.Entities.AddressMock.CompleteAddress, null));

        //    var result = _addressController.CreateAddress(Mocks.Entities.AddressMock.AddressInputModel);

        //    Assert.IsType<OkObjectResult>(result.Result);
        //}

        //[Fact]
        //public void GetAddress_ReturnSucess()
        //{
        //    _addressRepository.Setup(m => m.GetOneAsync(It.IsAny<Guid>())).ReturnsAsync((Mocks.Entities.AddressMock.CompleteAddress, null));
        //    _addressRepository.Setup(m => m.GetOneAsync(It.IsAny<string>())).ReturnsAsync((Mocks.Entities.AddressMock.Address2, null));

        //    var result = _addressController.GetAddressById(Mocks.Entities.AddressMock.Id);
        //    Assert.IsType<OkObjectResult>(result.Result);
        //}

        //[Fact]
        //public void UpdateAddress_ReturnSucess()
        //{
        //    _addressRepository.Setup(m => m.GetOneAsync(It.IsAny<string>())).ReturnsAsync((Mocks.Entities.AddressMock.Address, null));
        //    _completeAddressRepository.Setup(m => m.UpdateAsync(It.IsAny<Guid>(), It.IsAny<CompleteAddress>())).ReturnsAsync((Mocks.Entities.AddressMock.CompleteAddress, null));

        //    var result = _addressController.UpdateAddress(Mocks.Entities.AddressMock.Id, Mocks.Entities.AddressMock.AddressInputModel);
        //    Assert.IsType<OkObjectResult>(result.Result);
        //}
        //#endregion

        //#region Testes Negativos

        //[Fact]
        //public void CreateAddress_ReturnBadRequest()
        //{
        //    _viaCepService.Setup(m => m.GetAddressByZipCode(It.IsAny<string>())).ReturnsAsync(Mocks.Entities.AddressMock.Address);
        //    _addressRepository.Setup(m => m.CreateAddress(It.IsAny<Domain.Entities.Address>())).ReturnsAsync((null, "Erro"));
        //    _addressRepository.Setup(m => m.CreateCompleteAddress(It.IsAny<CompleteAddress>())).ReturnsAsync((false, "Erro"));
        //    _addressRepository.Setup(m => m.GetCompleteAddressById(It.IsAny<Guid>())).ReturnsAsync((null, "Erro"));
        //    _addressRepository.Setup(m => m.GetAddressByZipCode(It.IsAny<string>())).ReturnsAsync((null, "Erro"));

        //    var result = _addressController.CreateAddress(Mocks.Entities.AddressMock.AddressInputModel);

        //    Assert.IsType<BadRequestObjectResult>(result.Result);
        //}

        //[Fact]
        //public void CreateAddress_ReturnBadRequest_WhenViaCepApiDoNotRespond()
        //{
        //    _addressRepository.Setup(m => m.GetAddressByZipCode(It.IsAny<string>())).ReturnsAsync((null, null));

        //    var result = _addressController.CreateAddress(Mocks.Entities.AddressMock.AddressInputModel);

        //    Assert.IsType<BadRequestObjectResult>(result.Result);
        //    Assert.Equal("Endereço não encontrado", ((BadRequestObjectResult)result.Result).Value);
        //}

        //[Fact]
        //public void CreateAddress_ReturnBadRequest_WhenCreateAddressReturnNull()
        //{
        //    _viaCepService.Setup(m => m.GetAddressInAPI(It.IsAny<string>())).ReturnsAsync(Mocks.Entities.AddressMock.Address);
        //    _addressRepository.Setup(m => m.CreateAddress(It.IsAny<Domain.Entities.Address>())).ReturnsAsync((null, "Erro"));

        //    var result = _addressController.CreateAddress(Mocks.Entities.AddressMock.AddressInputModel);

        //    Assert.Equal("erro", ((BadRequestObjectResult)result.Result).Value);
        //}

        //[Fact]
        //public void CreateAddress_ReturnBadRequest_WhenCreateCompleteAddressReturnFalse()
        //{
        //    _viaCepService.Setup(m => m.GetAddressInAPI(It.IsAny<string>())).ReturnsAsync(Mocks.Entities.AddressMock.Address);
        //    _addressRepository.Setup(m => m.CreateAddress(It.IsAny<Domain.Entities.Address>())).ReturnsAsync((Mocks.Entities.AddressMock.Address, null));
        //    _addressRepository.Setup(m => m.CreateCompleteAddress(It.IsAny<CompleteAddress>())).ReturnsAsync((false, "erro"));


        //    var result = _addressController.CreateAddress(Mocks.Entities.AddressMock.AddressInputModel);

        //    Assert.Equal("erro", ((BadRequestObjectResult)result.Result).Value);
        //}

        //[Fact]
        //public void GetAddress_ReturnBadRequest()
        //{
        //    _addressRepository.Setup(m => m.GetCompleteAddressById(It.IsAny<Guid>())).ReturnsAsync((null, "Erro"));
        //    _addressRepository.Setup(m => m.GetAddressByZipCode(It.IsAny<string>())).ReturnsAsync((null, "Erro"));

        //    var result = _addressController.GetAddressById(Mocks.Entities.AddressMock.Id);
        //    Assert.IsType<BadRequestObjectResult>(result.Result);
        //}

        //[Fact]
        //public void UpdateAddress_ReturnBadRequest()
        //{
        //    _addressRepository.Setup(m => m.GetAddressByZipCode(It.IsAny<string>())).ReturnsAsync((null, "Erro"));
        //    _addressRepository.Setup(m => m.UpdateAddress(It.IsAny<Guid>(), It.IsAny<CompleteAddress>())).ReturnsAsync((null, "Erro"));

        //    var result = _addressController.UpdateAddress(Mocks.Entities.AddressMock.Id, Mocks.Entities.AddressMock.AddressInputModel);
        //    Assert.IsType<BadRequestObjectResult>(result.Result);
        //}

        #endregion
    }
}