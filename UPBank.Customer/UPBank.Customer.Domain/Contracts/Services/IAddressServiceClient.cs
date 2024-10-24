using UPBank.Customer.Domain.Services;

namespace UPBank.Customer.Domain.Contracts.Services
{
    public interface IAddressServiceClient
    {
        Task<AddressResponseData?> CreateAddress(AddressRequestData createAddress);
        Task<AddressResponseData?> UpdateAddress(string id, AddressRequestData updateAddress);
        Task<AddressResponseData?> GetCompleteAddressById(string id);
    }
}
