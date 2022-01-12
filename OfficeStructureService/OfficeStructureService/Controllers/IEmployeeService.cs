using OfficeStructureService.DTO.Employee;
using System.Collections.Generic;

namespace OfficeStructureService.Cotrollers
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployees();
        public List<Employee> GetEmployeesByDepartmentId(string departmentId);
        public Employee GetEmployeeById(string id);
        public string GetEmployee(Authorization auth);
        public bool IsEmployeeExist(string id);
    }
}
