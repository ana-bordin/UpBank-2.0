using Microsoft.AspNetCore.Mvc;
using UPBank.Address.Application.Contracts;
using UPBank.Address.Application.Models;

namespace UPBank.Address.API.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("api/addresses/{id}")]
        public async Task<IActionResult> GetAddressById(Guid id)
        {
            var addressOutputModel = await _addressService.GetCompleteAddressById(id);

            if (addressOutputModel.addressOutputModel == null && addressOutputModel.message == null)
                return NotFound();
            else if (addressOutputModel.message != null)
                return BadRequest(addressOutputModel.message);

            return Ok(addressOutputModel.addressOutputModel);
        }

        [HttpPost("api/addresses")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressInputModel addressInputModel)
        {
            var address = await _addressService.CreateAddress(addressInputModel.ZipCode);

            if (!address.ok)
                return BadRequest(address.message);

            var completeAddress = await _addressService.CreateCompleteAddress(addressInputModel);

            if (completeAddress.guid == Guid.Empty)
                return BadRequest(completeAddress.message);

            var addressOutputModel = await _addressService.GetCompleteAddressById(completeAddress.guid);

            return Ok(addressOutputModel.addressOutputModel);
        }

        [HttpDelete("api/addresses/{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var address = await _addressService.DeleteAddressById(id);

            if (!address.ok)
            {
                if (address.message == "Endereço não encontrado")
                    return NotFound(address.message);

                return BadRequest(address.message);
            }
            return Ok();
        }

        [HttpPatch("api/addresses/{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] AddressInputModel addressInputModel)
        {
            var address = await _addressService.UpdateAddress(id, addressInputModel);

            if (address.addressOutputModel == null)
                return BadRequest(address.message);

            return Ok(address.addressOutputModel);
        }

    }
}
