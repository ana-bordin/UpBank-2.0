using Microsoft.AspNetCore.Mvc;
using UPBank.Address.Application.Contracts;
using UPBank.Address.Application.Models;
using UPBank.Address.Domain.Entities;

namespace UPBank.Address.API.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IViaCepService _viaCep;

        public AddressController(IAddressService addressService, IViaCepService viaCep)
        {
            _addressService = addressService;
            _viaCep = viaCep;
        }

        //[HttpGet("api/addresses/api/{zipCode}")]
        //public async Task<IActionResult> GetAddressInAPI(string zipCode)
        //{
        //    var address = await _viaCep.GetAddressInAPI(zipCode);
        //    if (address == null)
        //    {
        //        return NotFound();
        //    }

        //    var adressOutputModel = new AddressOutputModel
        //    {
        //        ZipCode = address.ZipCode,
        //        Street = address.Street,
        //        Neighborhood = address.Neighborhood,
        //        City = address.City,
        //        State = address.State
        //    };

        //    return Ok(adressOutputModel);
        //}

        [HttpGet("api/addresses/{id}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var address = await _addressService.GetCompleteAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        [HttpPost("api/addresses")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressInputModel addressInputModel)
        {
            Domain.Entities.Address getAddressInViaCep = await _viaCep.GetAddressInAPI(addressInputModel.ZipCode);

            if (getAddressInViaCep == null)
                return NotFound();

            var address = await _addressService.CreateAddress(getAddressInViaCep);

            var completeAddressId = await _addressService.CreateCompleteAddress(addressInputModel);

            if (address == null || completeAddressId == Guid.Empty)
                return NotFound("Não foi possivel criar o endereço");

            AddressOutputModel addressOutputModel = new AddressOutputModel
            {
                Id = completeAddressId,
                ZipCode = getAddressInViaCep.ZipCode,
                Street = getAddressInViaCep.Street,
                Neighborhood = getAddressInViaCep.Neighborhood,
                City = getAddressInViaCep.City,
                State = getAddressInViaCep.State,
                Complement = addressInputModel.Complement,
                Number = addressInputModel.Number
            };

            return Ok(addressOutputModel);
        }

        [HttpPatch("api/addresses/{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] AddressInputModel addressInputModel)
        {
            var add = await _addressService.GetAddressByZipCode(addressInputModel.ZipCode);

            if (add == null)
            {
                add = await _viaCep.GetAddressInAPI(addressInputModel.ZipCode);

                if (add == null)
                    return NotFound();
                else
                    await _addressService.CreateAddress(add);
            }

            await _addressService.UpdateAddress(id, addressInputModel);

            AddressOutputModel addressOutputModel = new AddressOutputModel
            {
                Id = id,
                ZipCode = add.ZipCode,
                Street = add.Street,
                Neighborhood = add.Neighborhood,
                City = add.City,
                State = add.State,
                Complement = addressInputModel.Complement,
                Number = addressInputModel.Number
            };

            return Ok(addressOutputModel);
        }

        [HttpDelete("api/addresses/{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var address = await _addressService.GetCompleteAddressById(id);
            if (address == null)
            {
                return NotFound();
            }
            await _addressService.DeleteAddressById(id);
            return Ok();
        }
    }
}
