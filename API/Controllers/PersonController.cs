using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAll()
        {
            try
            {
                IEnumerable<PersonDTO> people = await _personService.GetAll();
                if (people == null)
                    return NotFound();
                return new ObjectResult(people);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            try
            {
                PersonDTO person = await _personService.Get(id);
                if (person == null)
                    return NotFound();
                return new ObjectResult(person);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PersonDTO>> Post([FromBody] PersonDTO person)
        {
            try
            {
                await _personService.Create(person);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<PersonDTO>> Put([FromBody]  PersonDTO person)
        {
            try
            {
                await _personService.Update(person);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult<PersonDTO>> Delete(int id)
        {
            try
            {
                var person = await _personService.Get(id);
                if (person == null)
                    return NotFound();
                await _personService.Delete(person);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
