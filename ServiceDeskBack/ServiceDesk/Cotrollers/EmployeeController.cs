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
        private readonly Token _token;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _token = new Token(_employeeService);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            try
            {
                if (!Request.Headers.ContainsKey("Token"))
                {
                    return Unauthorized();
                }
                await _token.CheckTokenAsync(Request.Headers["Token"]);
                return Ok(await _employeeService.GetEmployees());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetEmployeesByDepartmentIdAsync(string departmentId)
        {
            try
            {
                if (!Request.Headers.ContainsKey("Token"))
                {
                    return Unauthorized();
                }
                await _token.CheckTokenAsync(Request.Headers["Token"]);
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
                if (!Request.Headers.ContainsKey("Token"))
                {
                    return Unauthorized();
                }
                await _token.CheckTokenAsync(Request.Headers["Token"]);

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
            var result = await _employeeService.GetEmployee(auth);
            var t = _token.CreateToken(auth.login, auth.password, result.guid);
            Response.Headers.Add("Token", t);
            return Ok(result);
        }
    }
}
