using UPBank.Address.Application.Contracts;
using UPBank.Address.Application.Models;
using UPBank.Address.Domain.Entities;
using UPBank.Address.Infra.Repositories;

namespace UPBank.Address.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IViaCepService _viaCep;

        public AddressService(IAddressRepository addressRepository, IViaCepService viaCep)
        {
            _addressRepository = addressRepository;
            _viaCep = viaCep;
        }
        public async Task<(AddressOutputModel addressOutputModel, string message)> GetCompleteAddressById(Guid id)
        {
            var completeAddress = await _addressRepository.GetCompleteAddressById(id);
            var address = await _addressRepository.GetAddressByZipCode(completeAddress.completeAddress.ZipCode);

            if (completeAddress.completeAddress == null || address.address == null)
                return (null, completeAddress.message + address.message);

            return (await CreateAddressOutputModel(completeAddress.completeAddress, address.address), null);
        }

        public async Task<(bool ok, string message)> CreateAddress(string zipCode)
        {
            var getAddress = await GetAddressByZipCode(zipCode);
            if (getAddress.address == null)
            {
                var getAddressInViaCep = await _viaCep.GetAddressInAPI(zipCode);
                getAddress = await _addressRepository.CreateAddress(getAddressInViaCep);
            }

            if (getAddress.address == null)
                return (false, getAddress.message);

            return (true, getAddress.message);
        }

        public async Task<(Guid guid, string message)> CreateCompleteAddress(AddressInputModel addressInputModel)
        {
            var completeAddress = new CompleteAddress
            {
                Id = Guid.NewGuid(),
                Complement = addressInputModel.Complement,
                Number = addressInputModel.Number,
                ZipCode = addressInputModel.ZipCode
            };

            var completeAddressResult = await _addressRepository.CreateCompleteAddress(completeAddress);

            if (!completeAddressResult.ok)
                return (Guid.Empty, completeAddressResult.message);

            return (completeAddress.Id, null);
        }

        public async Task<(bool ok, string message)> DeleteAddressById(Guid id)
        {
            var address = await _addressRepository.GetCompleteAddressById(id);
            if (address.completeAddress == null)
                return (false, "Endereço não encontrado");

            return await _addressRepository.DeleteAddressById(id);
        }

        public async Task<(Domain.Entities.Address address, string message)> GetAddressByZipCode(string zipCode)
        {
            return await _addressRepository.GetAddressByZipCode(zipCode);        
        }

        public async Task<(AddressOutputModel addressOutputModel, string message)> UpdateAddress(Guid id, AddressInputModel addressInputModel)
        {
            Domain.Entities.Address getAddressOnViaCep;

                var ifAddressExists = await _addressRepository.GetAddressByZipCode(addressInputModel.ZipCode);

                if (ifAddressExists.address == null)
                {
                    getAddressOnViaCep = await _viaCep.GetAddressInAPI(addressInputModel.ZipCode);
                    ifAddressExists = (getAddressOnViaCep, null);
                    if (getAddressOnViaCep == null)
                        return (null, "Não foi possivel criar o endereço");
                }

            var address = new CompleteAddress
            {
                Complement = addressInputModel.Complement,
                Number = addressInputModel.Number,
                ZipCode = addressInputModel.ZipCode
            };

            var completeAddress = await _addressRepository.UpdateAddress(id, address);
            
            if (completeAddress.completeAddress == null)
                return (null, completeAddress.message);

            return (await CreateAddressOutputModel(completeAddress.completeAddress, ifAddressExists.address), null);
        }

        public async Task<AddressOutputModel> CreateAddressOutputModel(CompleteAddress completeAddress, Domain.Entities.Address address)
        {
            return new AddressOutputModel
            {
                City = address.City,
                Complement = completeAddress.Complement,
                Id = completeAddress.Id,
                Neighborhood = address.Neighborhood,
                Number = completeAddress.Number,
                State = address.State,
                Street = address.Street,
                ZipCode = completeAddress.ZipCode
            };

        }
    }
}
