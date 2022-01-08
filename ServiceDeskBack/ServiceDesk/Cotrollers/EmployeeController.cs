using Microsoft.AspNetCore.Mvc;
using ServiceDesk.DTO.Employee;
using System;

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
        public IActionResult GetEmployees()
        {
            return Ok(_employeeService.GetEmployees());
        }

        [HttpGet("department/{departmentId}")]
        public IActionResult GetEmployeesByDepartmentId(string departmentId)
        {
            try
            {
                return Ok(_employeeService.GetEmployeesByDepartmentId(departmentId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(string id)
        {
            try
            {
                return Ok(_employeeService.GetEmployeeById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("auth")]
        public IActionResult GetEmployee(Authorization auth)
        {
            try
            {
                return Ok(new Result
                {
                    ErrorCode = 0,
                    Guid = _employeeService.GetEmployee(auth)
                });
            }
            catch (Exception)
            {
                return Ok(new Result
                {
                    ErrorCode = 1,
                    Guid = ""
                });
            }
        }
    }

    public class Result
    {
        public int ErrorCode { get; set; }
        public string Guid { get; set; }
    }
}
