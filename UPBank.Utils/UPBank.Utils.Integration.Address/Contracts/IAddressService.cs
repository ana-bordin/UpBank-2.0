using UPBank.Address.API.Models;

namespace UPBank.Utils.Integration.Address.Contracts
{ 
    public interface IAddressService
    {
        Task<OutputAddressModel?> CreateAddress(InputAddressModel createAddress);
        Task<OutputAddressModel?> UpdateAddress(string id, InputAddressModel updateAddress);
        Task<OutputAddressModel?> GetCompleteAddressById(string id);
    }
}