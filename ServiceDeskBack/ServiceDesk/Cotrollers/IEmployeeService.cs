using ServiceDesk.DTO.Employee;
using System.Collections.Generic;

namespace ServiceDesk.Cotrollers
{
    public interface IEmployeeService
    {
        public List<Employee> GetEmployees();
        public List<Employee> GetEmployeesByDepartmentId(int departmentId);
        public Employee GetEmployeeById(int id);
        public int GetEmployee(Authorization auth);
    }
}
