using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OfficeStructureService.Cotrollers;
using OfficeStructureService.DTO.Employee;

namespace OfficeStructureService.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DBContext _db;
        private readonly string _baseUrl = "http://officestructure-001-site1.ctempurl.com/";
        public EmployeeService(DBContext db)
        {
            _db = db;
        }

        public string GetEmployee(Authorization auth)
        {
            var employee = _db.Employees.FirstOrDefault(x => (x.Login == auth.Login) && (x.Password == auth.Password));
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }
            return employee.Guid;
        }

        public Employee GetEmployeeById(string id)
        {
            var employee = _db.Employees.Include(x => x.Department).FirstOrDefault(x => x.Guid == id);
            if (employee == null)
            {
                throw new Exception("Unknown employee id");
            }
            return ConvertToEmployeeDTO(employee);
        }

        public List<Employee> GetEmployees()
        {
            return _db.Employees.Include(x => x.Department).ToList().ConvertAll<Employee>(ConvertToEmployeeDTO);
        }

        public List<Employee> GetEmployeesByDepartmentId(string departmentId)
        {
            return _db.Employees.Include(x => x.Department)
                .Where(e => departmentId != null
            ? e.Department != null && e.Department.Guid == departmentId
            : e.Department == null)
                .ToList().ConvertAll<Employee>(ConvertToEmployeeDTO);
        }

        public bool IsEmployeeExist(string id)
        {
            return _db.Employees.FirstOrDefault(x => x.Guid == id) != null;
        }

        private Employee ConvertToEmployeeDTO(Model.Employee employee)
        {
            var employeeDTO = new Employee()
            {
                Guid = employee.Guid,
                Name = employee.Name,
                PhotoPath = _baseUrl + employee.PhotoPath
            };

            if (employee.Department != null)
            {
                employeeDTO.Department = new Department()
                {
                    Guid = employee.Department.Guid,
                    Name = employee.Department.Name
                };
            }

            return employeeDTO;
        }
    }
}
