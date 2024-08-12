using MediatR;
using Microsoft.AspNetCore.Mvc;
using UPBank.Person.Domain.Commands.CreatePerson;

namespace UPBank.Person.API.Controllers
{
    public class PersonController : Controller
    {
        private readonly IMediator _bus;

        public PersonController(IMediator mediator)
        {
            _bus = mediator;
        }

        [HttpPost("api/peoples")]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand person)
        {
            var response = await _bus.Send(person);

            if (response.Errors.Count() != 0)
                return BadRequest(response.ErrorsResponse);
            
            return Ok(response);
        }

        //[HttpGet("api/peoples/{cpf}")]
        //public async Task<IActionResult> GetPerson(string cpf)
        //{
        //    var response = await _bus.Send(new GetPersonByCpfQuery(cpf));
        //    //var person = await _personService.GetPersonByCpf(cpf);

        //    //if (person.person == null && person.message == null)
        //    //    return NotFound("Pessoa não encontrada");

        //    //if (person.message != null)
        //    //    return BadRequest(person.message);

        //    //return Ok(person.person);
        //}

        //[HttpPatch("api/peoples/{cpf}")]
        //public async Task<IActionResult> UpdatePerson(string cpf, [FromBody] PersonPatchDTO personPatchDTO)
        //{
        //    var response = await _bus.Send(new UpdatePersonCommand(cpf, personPatchDTO));
        //    //cpf = Domain.Entities.Person.CpfRemoveMask(cpf);
        //    //(PersonOutputModel okResult, string message) getPerson = await _personService.GetPersonByCpf(cpf);

        //    //if (getPerson.okResult == null)
        //    //    return BadRequest(getPerson.message);

        //    //var ok = await _personService.PatchPerson(cpf, personPatchDTO);

        //    //if (ok.person == null && ok.message == null)
        //    //    return NotFound("Pessoa não encontrada");

        //    //else if (ok.message != null)
        //    //    return BadRequest(ok.message);

        //    //else
        //    //    return Ok(ok.person);
        ////}
    }
}