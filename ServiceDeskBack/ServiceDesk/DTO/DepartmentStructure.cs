using System.Collections.Generic;

namespace ServiceDesk.DTO.DepartmentStructure
{
    public class Department
    {
        public string guid { get; set; }
        public string name { get; set; }
    }

    public class Employee
    {
        public string guid { get; set; }
        public string name { get; set; }
        public string photoPath { get; set; }
    }

    public class DepartmentStructure
    {
        public List<Employee> employees { get; set; }
        public List<Department> departments { get; set; }
    }
}
