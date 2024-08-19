using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPBank.Person.API.Models;
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
        private readonly IMapper _mapper;

        public PersonController(IMediator mediator, IDomainNotificationService domainNotificationService, IMapper mapper)
        {
            _bus = mediator;
            _domainNotifications = domainNotificationService;
            _mapper = mapper;
        }

        [HttpPost("api/peoples")]
        public async Task<IActionResult> CreatePerson([FromBody] InputPersonModel person, CancellationToken cancellationToken)
        {
            var response = await _bus.Send(_mapper.Map<InputPersonModel, CreatePersonCommand>(person), cancellationToken);

            if (_domainNotifications.HasNotification)
                return BadRequest(_domainNotifications.Get());

            return Ok(_mapper.Map<CreatePersonCommandResponse, OutputPersonModel>(response));
        }

        [HttpGet("api/peoples/{cpf}")]
        public async Task<IActionResult> GetPerson(string cpf)
        {
            var response = await _bus.Send(new GetPersonByCPFQuery(cpf));

            if (_domainNotifications.HasNotification)
                return BadRequest(_domainNotifications.Get());

            return Ok(_mapper.Map<CreatePersonCommandResponse, OutputPersonModel>(response));
        }

        [HttpPatch("api/peoples/{cpf}")]
        public async Task<IActionResult> UpdatePerson(string cpf, [FromBody] InputPatchPersonModel person, CancellationToken cancellationToken)
        {
            UpdatePersonCommand personCommand = _mapper.Map<InputPatchPersonModel, UpdatePersonCommand>(person);
            _mapper.Map(cpf, personCommand);
            var response = await _bus.Send(personCommand, cancellationToken);

            if (_domainNotifications.HasNotification)
                return BadRequest(_domainNotifications.Get());

            return Ok(_mapper.Map<CreatePersonCommandResponse, OutputPersonModel>(response));
        }
    }
}