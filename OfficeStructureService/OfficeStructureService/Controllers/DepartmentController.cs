using Microsoft.AspNetCore.Mvc;

namespace OfficeStructureService.Cotrollers
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
        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(string id)
        {
            return Ok(_departmentService.GetDepartmentById(id));
        }
        [HttpGet("isexist/{id}")]
        public IActionResult IsDepartmentExist(string id)
        {
            return Ok(_departmentService.IsDepartmentExist(id));
        }
    }
}
