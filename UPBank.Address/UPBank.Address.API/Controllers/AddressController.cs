using Microsoft.AspNetCore.Mvc;
using UPBank.Address.Application.Contracts;
using UPBank.Address.Application.Models;

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

        [HttpGet("api/addresses/{id}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var completeAddress = await _addressService.GetCompleteAddressById(id);
            var address = await _addressService.GetAddressByZipCode(completeAddress.ZipCode);
            if (address == null)
                return NotFound();
            var addressOutputModel = new AddressOutputModel(completeAddress.Id, completeAddress.ZipCode, completeAddress.Complement, completeAddress.Number, address.Street, address.Neighborhood, address.City, address.State);

            return Ok(addressOutputModel);
        }

        [HttpPost("api/addresses")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressInputModel addressInputModel)
        {
            var getIfExists = await _addressService.GetAddressByZipCode(addressInputModel.ZipCode);
            Domain.Entities.Address getAddressInViaCep;

            if (getIfExists == null)
            {
                getAddressInViaCep = await _viaCep.GetAddressInAPI(addressInputModel.ZipCode);

                if (getAddressInViaCep == null)
                    return NotFound();

                await _addressService.CreateAddress(getAddressInViaCep);
            }
            else
                getAddressInViaCep = getIfExists;

            var completeAddressId = await _addressService.CreateCompleteAddress(addressInputModel);

            if (completeAddressId == Guid.Empty)
                return BadRequest("Não foi possivel criar o endereço");

            var addressOutputModel = new AddressOutputModel(completeAddressId, addressInputModel.ZipCode, addressInputModel.Complement, addressInputModel.Number, getAddressInViaCep.Street, getAddressInViaCep.Neighborhood, getAddressInViaCep.City, getAddressInViaCep.State);
   
            return Ok(addressOutputModel);
        }
       
        [HttpPatch("api/addresses/{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] AddressInputModel addressInputModel)
        {
            var getAddress = await _addressService.GetAddressByZipCode(addressInputModel.ZipCode);
            if (getAddress == null)
            {
                getAddress = await _viaCep.GetAddressInAPI(addressInputModel.ZipCode);
                if (getAddress == null)
                    return NotFound();
                else
                    await _addressService.CreateAddress(getAddress);
            }
           
            var completeAddress = await _addressService.UpdateAddress(id, addressInputModel);

            AddressOutputModel addressOutputModel = new AddressOutputModel(completeAddress.Id, addressInputModel.ZipCode, addressInputModel.Complement, addressInputModel.Number, getAddress.Street, getAddress.Neighborhood, getAddress.City, getAddress.State);

            return Ok(addressOutputModel);
        }

        [HttpDelete("api/addresses/{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var address = await _addressService.GetCompleteAddressById(id);
            
            if (address == null)
                return NotFound();
            
            await _addressService.DeleteAddressById(id);
            return Ok();
        }
    }
}
