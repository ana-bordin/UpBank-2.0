using Microsoft.AspNetCore.Mvc;
using UPBank.Person.Application.Contracts;
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
        public async Task<IActionResult> CreatePerson([FromBody] Domain.Entities.Person person)
        {
            if (!await _personService.CheckIfExist(person.CPF))
                await _personService.CreatePerson(person);
            person.CPF = person.CpfAddMask(person.CPF);
            return Ok(person);
        }

        [HttpGet("api/peoples/{cpf}")]
        public async Task<IActionResult> GetPerson(string cpf)
        {
            var person = await _personService.GetPersonByCpf(cpf);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPatch("api/peoples/{cpf}")]
        public async Task<IActionResult> UpdateCustomer(string cpf, [FromBody] PersonPatchDTO personPatchDTO)
        {
            var ok = await _personService.PatchPerson(cpf, personPatchDTO);

            if (ok == null)
                return BadRequest();
            else
                return Ok(ok);
        }
    }
}
