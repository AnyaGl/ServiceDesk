using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Cotrollers;
using ServiceDesk.DTO.Employee;

namespace ServiceDesk.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DBContext _db;
        public EmployeeService(DBContext db)
        {
            _db = db;
        }
        public Employee GetEmployeeById(int id)
        {
            var employee = _db.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == id);
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

        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            return _db.Employees.Include(x => x.Department).Where(e => e.Department.Id == departmentId).ToList().ConvertAll<Employee>(ConvertToEmployeeDTO);
        }

        private Employee ConvertToEmployeeDTO(Model.Employee employee)
        {
            var employeeDTO = new Employee()
            {
                Id = employee.Id,
                Name = employee.Name
            };

            if (employee.Department != null)
            {
                employeeDTO.Department = new Department()
                {
                    Id = employee.Department.Id,
                    Name = employee.Department.Name
                };
            }

            return employeeDTO;
        }
    }
}
