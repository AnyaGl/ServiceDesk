using ServiceDesk.DTO.Department;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceDesk.Cotrollers
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetDepartmentsHierarchy();
        public Task<Department> GetDepartmentById(string id);
        public Task<bool> IsDepartmentExist(string id);
    }
}
