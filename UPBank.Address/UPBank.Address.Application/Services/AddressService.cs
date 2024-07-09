using UPBank.Address.Application.Contracts;
using UPBank.Address.Application.Models;
using UPBank.Address.Domain.Entities;
using UPBank.Address.Infra.Repositories;

namespace UPBank.Address.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<bool> CreateAddress(Domain.Entities.Address address)
        {
            address.FormatZipCode();
            return await _addressRepository.CreateAddress(address);
        }

        public async Task<Guid> CreateCompleteAddress(AddressInputModel addressInputModel)
        {
            var address = new CompleteAddress
            {
                Complement = addressInputModel.Complement,
                Number = addressInputModel.Number,
                ZipCode = addressInputModel.ZipCode
            };

            return await _addressRepository.CreateCompleteAddress(address);
        }

        public async Task<CompleteAddress> GetCompleteAddressById(Guid id)
        {
            return await _addressRepository.GetCompleteAddressById(id);
        }

        public async Task<Domain.Entities.Address> GetAddressByZipCode(string zipCode)
        {
            return await _addressRepository.GetAddressByZipCode(zipCode);
        }

        public async Task<CompleteAddress> UpdateAddress(Guid id, AddressInputModel addressInputModel)
        {
            var address = new CompleteAddress
            {
                Complement = addressInputModel.Complement,
                Number = addressInputModel.Number,
                ZipCode = addressInputModel.ZipCode
            };

            return await _addressRepository.UpdateAddress(id, address);
        }

        public async Task<bool> DeleteAddressById(Guid id)
        {
            return await _addressRepository.DeleteAddressById(id);
        }
    }
}
