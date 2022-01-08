using System.Collections.Generic;

namespace ServiceDesk.DTO.DepartmentStructure
{
    public class Department
    {
        public string Guid { get; set; }
        public string Name { get; set; }
    }

    public class Employee
    {
        public string Guid { get; set; }
        public string Name { get; set; }
    }

    public class DepartmentStructure
    {
        public List<Employee> Employees { get; set; }
        public List<Department> Departments { get; set; }
    }
}
