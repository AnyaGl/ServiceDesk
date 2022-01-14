using Microsoft.AspNetCore.Mvc;
using ServiceDesk.DTO.DepartmentStructure;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceDesk.Cotrollers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentStructureController : Controller
    {
        private readonly string _baseUrl = "http://officestructure-001-site1.ctempurl.com/api/DepartmentStructure/";
        private readonly Token _token;
        public DepartmentStructureController(IEmployeeService employeeService)
        {
            _token = new Token(employeeService);
        }

        [HttpGet("structure")]
        public async Task<IActionResult> GetStructureAsync(string departmentId)
        {
            if (!Request.Headers.ContainsKey("Token"))
            {
                return Unauthorized();
            }
            await _token.CheckTokenAsync(Request.Headers["Token"]);

            using (var httpClient = new HttpClient())
            {
                var url = $"{_baseUrl}structure";
                var httpResponse = await httpClient.GetAsync(departmentId != null ? $"{url}?departmentId={departmentId}" : url);
                var str = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                return Ok(JsonSerializer.Deserialize<DepartmentStructure>(str));
            }
        }
    }
}
