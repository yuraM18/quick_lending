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
    public class StatementTypeController : ControllerBase
    {
        private readonly IStatementTypeService _statementTypeService;

        public StatementTypeController(IStatementTypeService statementTypeService)
        {
            _statementTypeService = statementTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatementTypeDTO>>> GetAll()
        {
            try
            {
                IEnumerable<StatementTypeDTO> statementTypes = await _statementTypeService.GetAll();
                if (statementTypes == null)
                    return NotFound();
                return new ObjectResult(statementTypes);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatementTypeDTO>> Get([FromRoute] int id)
        {
            try
            {
                StatementTypeDTO statementType = await _statementTypeService.Get(id);
                if (statementType == null)
                    return NotFound();
                return new ObjectResult(statementType);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<StatementTypeDTO>> Post([FromBody] StatementTypeDTO statementType)
        {
            try
            {
                //var name = data["name"].FirstOrDefault();
                //var percentage = data["percentage"].FirstOrDefault();
                //var maxAmount = data["maxAmount"].FirstOrDefault();
                //var minAmount = data["minAmount"].FirstOrDefault();
                //var maxTerm = data["maxTerm"].FirstOrDefault();
                //var minTerm = data["minTerm"].FirstOrDefault();
                //StatementTypeDTO statementType = new StatementTypeDTO();
                //statementType.Name = name;
                //statementType.Percentage = short.Parse(percentage);
                //statementType.MaxAmount = int.Parse(maxAmount);
                //statementType.MinAmount = int.Parse(minAmount);
                //statementType.MaxTerm = int.Parse(maxTerm);
                //statementType.MinTerm = int.Parse(minTerm);

                await _statementTypeService.Create(statementType);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<ActionResult<StatementTypeDTO>> Put([FromBody] StatementTypeDTO statementType)
        {
            try
            {
                await _statementTypeService.Update(statementType);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StatementTypeDTO>> Delete(int id)
        {
            try
            {
                StatementTypeDTO statementType = await _statementTypeService.Get(id);
                if (statementType == null)
                    return NotFound();
                await _statementTypeService.Delete(statementType);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
