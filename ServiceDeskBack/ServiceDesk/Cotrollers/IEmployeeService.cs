using ServiceDesk.DTO.Employee;
using System.Collections.Generic;

namespace ServiceDesk.Cotrollers
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployees();
        public List<Employee> GetEmployeesByDepartmentId(string departmentId);
        public Employee GetEmployeeById(string id);
        public string GetEmployee(Authorization auth);
    }
}
