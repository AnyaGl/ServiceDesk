using Microsoft.AspNetCore.Mvc;

namespace ServiceDesk.Cotrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("hierarchy")]
        public IActionResult GetDepartmentsHierarchy()
        {
            return Ok(_departmentService.GetDepartmentsHierarchy());
        }
    }
}
