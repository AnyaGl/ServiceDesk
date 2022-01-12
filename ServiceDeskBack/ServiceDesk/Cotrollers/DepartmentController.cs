using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetDepartmentsHierarchyAsync()
        {
            return Ok(await _departmentService.GetDepartmentsHierarchy());
        }
    }
}
