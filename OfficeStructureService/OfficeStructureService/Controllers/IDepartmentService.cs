using OfficeStructureService.DTO.Department;
using System.Collections.Generic;

namespace OfficeStructureService.Cotrollers
{
    public interface IDepartmentService
    {
        public List<Department> GetDepartmentsHierarchy();
        public List<Department> GetDepartmentsByMain(string mainId);
        public Department GetDepartmentById(string id);
        public bool IsDepartmentExist(string id);
    }
}
