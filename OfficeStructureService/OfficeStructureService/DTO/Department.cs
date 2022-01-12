using System.Collections.Generic;

namespace OfficeStructureService.DTO.Department
{
    public class Department
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public List<Department> Subdepartments { get; set; }
    }
}
