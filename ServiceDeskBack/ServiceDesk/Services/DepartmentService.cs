using ServiceDesk.Cotrollers;
using ServiceDesk.DTO.Department;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceDesk.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly string _baseUrl = "http://officestructure-001-site1.ctempurl.com/api/Department/";

        public async Task<Department> GetDepartmentById(string id)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync($"{_baseUrl}{id}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var str = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<Department>(str);
                }
                return null;
            }
        }

        public async Task<List<Department>> GetDepartmentsHierarchy()
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync($"{_baseUrl}hierarchy");
                var str = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<List<Department>>(str);
            }
        }

        public async Task<bool> IsDepartmentExist(string id)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync($"{_baseUrl}isexist/{id}");
                var str = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<bool>(str);
            }
        }
    }
}
