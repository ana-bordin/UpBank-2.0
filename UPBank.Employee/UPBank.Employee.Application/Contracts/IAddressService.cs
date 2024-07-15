using UPBank.Address.Application.Models;
using UPBank.Address.Domain.Entities;

namespace UPBank.Utils.Address.Contracts
{
    public interface IAddressService
    {
        Task<AddressOutputModel> CreateAddress(AddressInputModel addressInputModel);
        Task<CompleteAddress> UpdateAddress(Guid Id, AddressInputModel addressInputModel);
        ////Task<Models.Address> DeleteAddress(Guid id);
        //Task<Models.Address> GetAddressByZipCode(string zipCode);
        Task<AddressOutputModel> GetCompleteAddressById(Guid id);
    }
}
