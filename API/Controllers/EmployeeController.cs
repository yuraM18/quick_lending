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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;


        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAll()
        {
            try
            {
                IEnumerable<EmployeeDTO> employees = await _employeeService.GetAll();
                if (employees == null)
                    return NotFound();
                return new ObjectResult(employees);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<EmployeeDTO>> Get(int id)
        {
            try
            {
                EmployeeDTO employee = await _employeeService.Get(id);
                if (employee == null)
                    return NotFound();
                return new ObjectResult(employee);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> Post([FromForm] EmployeeDTO employee)
        {
            try
            {
                await _employeeService.Create(employee);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDTO>> Delete(int id)
        {
            try
            {
                EmployeeDTO employee = await _employeeService.Get(id);
                if (employee == null)
                    return NotFound();
                await _employeeService.Delete(employee);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
