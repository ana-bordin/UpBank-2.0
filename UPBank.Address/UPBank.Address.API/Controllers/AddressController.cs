using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Commands.DeleteAddress;
using UPBank.Address.Domain.Commands.UpdateAddress;
using UPBank.Address.Domain.Queries.GetAddressById;

namespace UPBank.Address.API.Controllers
{
    public class AddressController : Controller
    {
        private readonly IMediator _bus;

        public AddressController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpGet("api/addresses/{getAddressByIdQuery.Id}")]
        public async Task<IActionResult> GetAddressById(GetAddressByIdQuery getAddressByIdQuery, CancellationToken cancellationToken)
        {
            var response = await _bus.Send(getAddressByIdQuery, cancellationToken);

            if (response.Errors.Count() != 0)
                return NotFound(response.Errors);

            return Ok(response);
        }

        [HttpPost("api/addresses")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand createAddressModel, CancellationToken cancellationToken)
        {
            var response = await _bus.Send(createAddressModel, cancellationToken);

            if (response.Errors != null)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpPatch("api/addresses/{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateAddressCommand updateAddressModel, CancellationToken cancellationToken)
        {
            updateAddressModel.Id = id;
            var response = await _bus.Send(updateAddressModel, cancellationToken);

            if (response.Errors.Count() != 0)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        [HttpDelete("api/addresses/{deleteAddressCommand.Id}")]
        public async Task<IActionResult> DeleteAddress(DeleteAddressCommand deleteAddressCommand, CancellationToken cancellationToken)
        {
            var response = await _bus.Send(deleteAddressCommand, cancellationToken);

            if (response.HasNotification)
                return BadRequest(response.Get());

            return Ok("Endereço excluido com sucesso!");
        }
    }
}