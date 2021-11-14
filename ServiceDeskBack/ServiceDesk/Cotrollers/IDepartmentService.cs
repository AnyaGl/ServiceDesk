using ServiceDesk.DTO.Department;
using System.Collections.Generic;

namespace ServiceDesk.Cotrollers
{
    public interface IDepartmentService
    {
        public List<Department> GetDepartmentsHierarchy();
    }
}
