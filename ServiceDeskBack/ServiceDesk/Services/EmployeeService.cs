using System;
using System.Collections.Generic;
using System.Linq;
using ServiceDesk.Cotrollers;
using ServiceDesk.Model;

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
            var employee = _db.Employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                throw new Exception("Unknown employee id");
            }
            return employee;
        }

        public List<Employee> GetEmployees()
        {
            return _db.Employees.ToList();
        }

        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            return _db.Employees.Where(e => e.DepartmentId == departmentId).ToList();
        }
    }
}
