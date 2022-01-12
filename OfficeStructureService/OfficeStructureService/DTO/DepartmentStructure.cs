using System.Collections.Generic;

namespace OfficeStructureService.DTO.DepartmentStructure
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
        public string PhotoPath { get; set; }
    }

    public class DepartmentStructure
    {
        public List<Employee> Employees { get; set; }
        public List<Department> Departments { get; set; }
    }
}
