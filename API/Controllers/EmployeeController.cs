using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            IEnumerable<EmployeeDTO> employees = await _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("id")]
        public async Task<ActionResult<EmployeeDTO>> Get(int id)
        {

            EmployeeDTO employee = await _employeeService.Get(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> Post([FromForm] EmployeeDTO employee)
        {

            await _employeeService.Create(employee);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDTO>> Delete(int id)
        {
            EmployeeDTO employee = await _employeeService.Get(id);
            await _employeeService.Delete(employee);
            return Ok();
        }
    }
}
