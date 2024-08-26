using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Person.Domain.Commands.UpdatePerson;
using UPBank.Person.Domain.Queries.GetPersonByCPF;
using UPBank.Utils.CrossCutting.Exception.Contracts;

namespace UPBank.Person.API.Controllers
{
    public class PersonController : Controller
    {
        private readonly IMediator _bus;
        private readonly IDomainNotificationService _domainNotifications;

        public PersonController(IMediator mediator, IDomainNotificationService domainNotificationService)
        {
            _bus = mediator;
            _domainNotifications = domainNotificationService;
        }

        [HttpPost("api/peoples")]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand createPersonCommand, CancellationToken cancellationToken)
        {
            var response = await _bus.Send(createPersonCommand, cancellationToken);

            if (_domainNotifications.HasNotification)
                return BadRequest(_domainNotifications.Get());

            return Ok(response);
        }

        [HttpGet("api/peoples/{cpf}")]
        public async Task<IActionResult> GetPerson(string cpf)
        {
            var response = await _bus.Send(new GetPersonByCPFQuery(cpf));

            if (_domainNotifications.HasNotification)
                return BadRequest(_domainNotifications.Get());

            return Ok(response);
        }

        [HttpPatch("api/peoples/{cpf}")]
        public async Task<IActionResult> UpdatePerson(string cpf, [FromBody] UpdatePersonCommand updatePersonCommand, CancellationToken cancellationToken)
        {
            updatePersonCommand.CPF = cpf;
            var response = await _bus.Send(updatePersonCommand, cancellationToken);

            if (_domainNotifications.HasNotification)
                return BadRequest(_domainNotifications.Get());

            return Ok(response);
        }
    }
}