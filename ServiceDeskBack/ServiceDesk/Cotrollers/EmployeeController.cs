using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetEmployeesByDepartmentId(int departmentId)
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
        public IActionResult GetEmployeeById(int id)
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
    }
}
