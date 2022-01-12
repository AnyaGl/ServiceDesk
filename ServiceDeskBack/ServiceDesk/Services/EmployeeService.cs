using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ServiceDesk.Cotrollers;
using ServiceDesk.DTO.Employee;

namespace ServiceDesk.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly string _baseUrl = "http://officestructure-001-site1.ctempurl.com/api/Employee/";

        public async Task<AuthResult> GetEmployee(Authorization auth)
        {
            var str = JsonSerializer.Serialize(auth);
            var httpContent = new StringContent(str, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var httpResponse = await httpClient.PostAsync($"{_baseUrl}auth", httpContent);
                    return JsonSerializer.Deserialize<AuthResult>(await httpResponse.Content.ReadAsStringAsync());
                }
                catch (Exception)
                {
                    return new AuthResult
                    {
                        errorCode = 1,
                        guid = ""
                    };
                }
            }
        }

        public async Task<Employee> GetEmployeeById(string id)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync($"{_baseUrl}{id}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var str = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<Employee>(str);
                }
                return null;
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync($"{_baseUrl}all");
                var str = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<List<Employee>>(str);
            }
        }

        public async Task<List<Employee>> GetEmployeesByDepartmentId(string departmentId)
        {
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync($"{_baseUrl}department/{departmentId}");
                var str = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonSerializer.Deserialize<List<Employee>>(str);
            }
        }

        public async Task<bool> IsEmployeeExist(string id)
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
