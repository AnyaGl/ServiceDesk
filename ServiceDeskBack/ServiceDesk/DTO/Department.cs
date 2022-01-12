using System.Collections.Generic;

namespace ServiceDesk.DTO.Department
{
    public class Department
    {
        public string guid { get; set; }
        public string name { get; set; }
        public List<Department> subdepartments { get; set; }
    }
}
