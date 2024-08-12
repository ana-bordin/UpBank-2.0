using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Commands.DeleteAddress;
using UPBank.Address.Domain.Commands.UpdateAddress;
using UPBank.Address.Domain.Queries.GetAddressById;
using UPBank.Utils.CommonsFiles.Contracts;

namespace UPBank.Address.API.Controllers
{
    public class AddressController : Controller
    {
        private readonly IMediator _bus;
        private readonly IDomainNotificationService _domainNotificationService;

        public AddressController(IMediator bus, IDomainNotificationService domainNotificationService)
        {
            _bus = bus;
            _domainNotificationService = domainNotificationService;
        }

        [HttpGet("api/addresses/{id}")]
        public async Task<IActionResult> GetAddressById(string id, CancellationToken cancellationToken)
        {
            var response = await _bus.Send(new GetAddressByIdQuery(Guid.Parse(id)), cancellationToken);

            if (_domainNotificationService.HasNotification)
                return NotFound(_domainNotificationService.Get);

            return Ok(response);
        }

        [HttpPost("api/addresses")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand createAddressModel, CancellationToken cancellationToken)
        {
            var response = await _bus.Send(createAddressModel, cancellationToken);

            if (_domainNotificationService.HasNotification)
                return NotFound(_domainNotificationService.Get);

            return Ok(response);
        }

        [HttpPatch("api/addresses/{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateAddressCommand updateAddressModel, CancellationToken cancellationToken)
        {
            updateAddressModel.Id = id;
            var response = await _bus.Send(updateAddressModel, cancellationToken);

            if (_domainNotificationService.HasNotification)
                return NotFound(_domainNotificationService.Get);

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