using Microsoft.AspNetCore.Mvc;
using ServiceDesk.DTO.Employee;
using System;
using System.Threading.Tasks;

namespace ServiceDesk.Cotrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            return Ok(await _employeeService.GetEmployees());
        }

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetEmployeesByDepartmentIdAsync(string departmentId)
        {
            try
            {
                return Ok(await _employeeService.GetEmployeesByDepartmentId(departmentId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(string id)
        {
            try
            {
                return Ok(await _employeeService.GetEmployeeById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("auth")]
        public async Task<IActionResult> GetEmployeeAsync(Authorization auth)
        {
            return Ok(await _employeeService.GetEmployee(auth));
        }
    }


}
