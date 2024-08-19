using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPBank.Address.API.Models;
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
        private readonly IMapper _mapper;

        public AddressController(IMediator bus, IDomainNotificationService domainNotificationService, IMapper mapper)
        {
            _bus = bus;
            _domainNotificationService = domainNotificationService;
            _mapper = mapper;
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
        public async Task<IActionResult> CreateAddress([FromBody] InputAddressModel inputAddressModel, CancellationToken cancellationToken)
        {
            var response = await _bus.Send(_mapper.Map<CreateAddressCommand>(inputAddressModel), cancellationToken);

            if (_domainNotificationService.HasNotification)
                return NotFound(_domainNotificationService.Get());
            
            return Ok(_mapper.Map<CreateAddressCommandResponse, OutputAddressModel>(response));
        }

        [HttpPatch("api/addresses/{id}")]
        public async Task<IActionResult> UpdateAddress(string id, [FromBody] InputAddressModel inputAddressModel, CancellationToken cancellationToken)
        {
            var commandAddress = _mapper.Map<UpdateAddressCommand>(inputAddressModel);
            commandAddress.Id = Guid.Parse(id);

            var response = await _bus.Send(commandAddress, cancellationToken);

            if (_domainNotificationService.HasNotification)
                return NotFound(_domainNotificationService.Get());

            return Ok(_mapper.Map<CreateAddressCommandResponse, OutputAddressModel>(response));
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