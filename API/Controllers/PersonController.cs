using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
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
            IEnumerable<PersonDTO> people = await _personService.GetAll();
            return Ok(people);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> Get(int id)
        {
            PersonDTO person = await _personService.Get(id);
            return Ok(person);
        }

        [HttpGet("{currentPage},{itemsOnPage}")]
        public async Task<ActionResult<PersonDTO>> GetMany(int currentPage, int itemsOnPage, bool? sortingDirection)
        {
            if (currentPage <= 0)
            {
                currentPage = 1;
            }
            if (itemsOnPage <= 0)
            {
                itemsOnPage = 10;
            }
            IEnumerable<PersonDTO> person = await _personService.GetMany(currentPage, itemsOnPage, sortingDirection);
            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDTO>> Post([FromBody] PersonDTO person)
        {
            await _personService.Create(person);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<PersonDTO>> Put([FromBody] PersonDTO person)
        {
            await _personService.Update(person);
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult<PersonDTO>> Delete(int id)
        {
            var person = await _personService.Get(id);
            await _personService.Delete(person);
            return Ok();
        }
    }
}
