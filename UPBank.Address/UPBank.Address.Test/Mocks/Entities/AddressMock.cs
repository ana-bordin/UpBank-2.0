using UPBank.Address.Application.Models;
using UPBank.Address.Domain.Entities;

namespace UPBank.Address.Test.Mocks.Entities
{
    public class AddressMock
    {
        public readonly Domain.Entities.Address address = new Domain.Entities.Address
        {
            ZipCode = "15997088",
            Street = "Rua 1",
            Neighborhood = "Bairro 1",
            City = "Cidade 1",
            State = "Estado 1"
        };

        public readonly Domain.Entities.Address address2 = new Domain.Entities.Address
        {
            ZipCode = "15997088",
            Street = "Rua 1",
            Neighborhood = "Bairro 1",
            City = "Cidade 1",
            State = "Estado 1"
        };

        public readonly AddressInputModel addressInputModel = new AddressInputModel
        {
            ZipCode = "15997088",
            Number = "123",
            Complement = "Complement"
        };

        public readonly CompleteAddress completeAddress = new CompleteAddress
        {
            Id = id,
            ZipCode = "15997088",
            Number = "123",
            Complement = "Complement"
        };

        public readonly Guid id = Guid.NewGuid();


    }
}
