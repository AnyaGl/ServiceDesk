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

        [HttpGet("get-employees")]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeService.GetEmployees());
        }

        [HttpGet("get-employees-by-department-id/{departmentId}")]
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

        [HttpGet("get-employee-by-id/{id}")]
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
                return Ok(_employeeService.GetEmployee(auth));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
