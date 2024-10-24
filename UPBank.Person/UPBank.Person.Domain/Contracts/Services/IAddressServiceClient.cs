using UPBank.Person.Domain.Commands.CreatePerson.Models.Address;

namespace UPBank.Person.Domain.Contracts.Services
{
    public interface IAddressServiceClient
    {
        Task<AddressResponse?> CreateAddress(AddressRequest createAddress);
        Task<AddressResponse?> UpdateAddress(string id, AddressRequest updateAddress);
        Task<AddressResponse?> GetCompleteAddressById(string id);
    }
}