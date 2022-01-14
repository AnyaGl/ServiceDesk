using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ServiceDesk.Cotrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService; 
        private readonly Token _token;
        public DepartmentController(IDepartmentService departmentService, IEmployeeService employeeService)
        {
            _departmentService = departmentService;
            _token = new Token(employeeService);
        }

        [HttpGet("hierarchy")]
        public async Task<IActionResult> GetDepartmentsHierarchyAsync()
        {
            if (!Request.Headers.ContainsKey("Token"))
            {
                return Unauthorized();
            }
            await _token.CheckTokenAsync(Request.Headers["Token"]);
            return Ok(await _departmentService.GetDepartmentsHierarchy());
        }
    }
}
