using Microsoft.AspNetCore.Mvc;
using ServiceDesk.DTO.DepartmentStructure;
using System.Collections.Generic;

namespace ServiceDesk.Cotrollers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentStructureController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        public DepartmentStructureController(IDepartmentService departmentService, IEmployeeService employeeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        [HttpGet("structure")]
        public IActionResult GetStructure(string departmentId)
        {
            var departments = _departmentService.GetDepartmentsByMain(departmentId);
            var employees = _employeeService.GetEmployeesByDepartmentId(departmentId);
            var result = new DepartmentStructure()
            {
                Departments = new List<Department>(),
                Employees = new List<Employee>()
            };

            foreach (var d in departments)
            {
                result.Departments.Add(new Department
                {
                    Guid = d.Guid,
                    Name = d.Name
                });
            }

            foreach (var e in employees)
            {
                result.Employees.Add(new Employee
                {
                    Guid = e.Guid,
                    Name = e.Name
                });
            }
            return Ok(result);
        }
    }
}
