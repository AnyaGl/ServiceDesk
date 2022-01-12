using ServiceDesk.DTO.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceDesk.Cotrollers
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetEmployees();
        public Task<List<Employee>> GetEmployeesByDepartmentId(string departmentId);
        public Task<Employee> GetEmployeeById(string id);
        public Task<bool> IsEmployeeExist(string id);
        public Task<AuthResult> GetEmployee(Authorization auth);
    }
}
