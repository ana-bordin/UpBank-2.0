using UPBank.Address.Domain.Contracts;
using UPBank.Address.Infra.Repositories;

namespace UPBank.Address.Domain.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<bool> CreateAddress(Entities.Address address)
        {
            return await _addressRepository.CreateAddress(address);
        }

        public async Task<Guid> CreateCompleteAddress(Entities.CompleteAddress addressInputModel)
        {
            return await _addressRepository.CreateCompleteAddress(addressInputModel);
        }

        public async Task<Entities.CompleteAddress> GetCompleteAddressById(Guid id)
        {
            return await _addressRepository.GetCompleteAddressById(id);
        }

        public async Task<Entities.Address> GetAddressByZipCode(string zipCode)
        {
            return await _addressRepository.GetAddressByZipCode(zipCode);
        }

        public async Task<Entities.CompleteAddress> UpdateAddress(Guid id, Entities.CompleteAddress addressInputModel)
        {
            return await _addressRepository.UpdateAddress(id, addressInputModel);
        }

        public async Task<bool> DeleteAddressById(Guid id)
        {
            return await _addressRepository.DeleteAddressById(id);
        }
    }
}
