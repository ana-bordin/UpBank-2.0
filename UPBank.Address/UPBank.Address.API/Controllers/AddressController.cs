using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Commands.DeleteAddress;
using UPBank.Address.Domain.Commands.UpdateAddress;
using UPBank.Address.Domain.Queries.GetAddressById;
using UPBank.Utils.CrossCutting.Exception.Contracts;

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
                return NotFound(_domainNotificationService.Get());

            return Ok(response);
        }

        [HttpPost("api/addresses")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand createAddressCommand, CancellationToken cancellationToken)
        {
            var response = await _bus.Send(createAddressCommand, cancellationToken);

            if (_domainNotificationService.HasNotification)
                return NotFound(_domainNotificationService.Get());

            return Ok(response);
        }

        [HttpPatch("api/addresses/{id}")]
        public async Task<IActionResult> UpdateAddress(string id, [FromBody] UpdateAddressCommand updateAddressCommand, CancellationToken cancellationToken)
        {
            updateAddressCommand.Id = Guid.Parse(id);
            var response = await _bus.Send(updateAddressCommand, cancellationToken);

            if (_domainNotificationService.HasNotification)
                return NotFound(_domainNotificationService.Get());

            return Ok(response);
        }

        [HttpDelete("api/addresses/{id}")]
        public async Task<IActionResult> DeleteAddress(string id, CancellationToken cancellationToken)
        {
            var response = await _bus.Send(new DeleteAddressCommand(Guid.Parse(id)), cancellationToken);

            if (response.HasNotification)
                return BadRequest(response.Get());

            return Ok("Endereço excluido com sucesso!");
        }
    }
}