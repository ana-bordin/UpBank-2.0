using Microsoft.AspNetCore.Mvc;
using UPBank.Person.Application.Contracts;
using UPBank.Person.Application.Models;
using UPBank.Person.Application.Models.DTOs;

namespace UPBank.Person.API.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("api/peoples")]
        public async Task<IActionResult> CreatePerson([FromBody] PersonInputModel person)
        {
            var personResult = await _personService.CreatePerson(person);

            if (personResult.okResult != null)
                return Ok(personResult.okResult);
            else
                return BadRequest(personResult.message);
        }

        [HttpGet("api/peoples/{cpf}")]
        public async Task<IActionResult> GetPerson(string cpf)
        {
            var person = await _personService.GetPersonByCpf(cpf);

            if (person.person == null && person.message == null)
                return NotFound("Pessoa não encontrada");

            if (person.message != null)
                return BadRequest(person.message);

            return Ok(person.person);
        }

        [HttpPatch("api/peoples/{cpf}")]
        public async Task<IActionResult> UpdatePerson(string cpf, [FromBody] PersonPatchDTO personPatchDTO)
        {
            cpf = Domain.Entities.Person.CpfRemoveMask(cpf);
            (PersonOutputModel okResult, string message) getPerson = await _personService.GetPersonByCpf(cpf);

            if (getPerson.okResult == null)
                return BadRequest(getPerson.message);

            var ok = await _personService.PatchPerson(cpf, personPatchDTO);

            if (ok.person == null && ok.message == null)
                return NotFound("Pessoa não encontrada");

            else if (ok.message != null)
                return BadRequest(ok.message);

            else
                return Ok(ok.person);
        }
    }
}