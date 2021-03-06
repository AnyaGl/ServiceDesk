using Microsoft.EntityFrameworkCore;
using OfficeStructureService.Cotrollers;
using OfficeStructureService.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OfficeStructureService.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DBContext _db;
        public DepartmentService(DBContext db)
        {
            _db = db;
        }

        public Department GetDepartmentById(string id)
        {
            var department = _db.Departments.FirstOrDefault(x => x.Guid == id);
            if (department == null)
            {
                throw new Exception("Unknown department id");
            }
            return new Department
            {
                Guid = department.Guid,
                Name = department.Name
            };
        }

        public List<Department> GetDepartmentsByMain(string mainId)
        {
            List<Department> result = new List<Department>();
            var departments = _db.Departments.Include(x => x.MainDepartment).ToList();
            foreach (var department in departments)
            {
                if (mainId != null
                    ? department.MainDepartment != null && department.MainDepartment.Guid == mainId
                    : department.MainDepartment == null)
                {
                    result.Add(new Department
                    {
                        Guid = department.Guid,
                        Name = department.Name
                    });
                }
            }
            return result;
        }

        public List<Department> GetDepartmentsHierarchy()
        {
            var departments = _db.Departments.Include(x => x.MainDepartment).ToList();
            return ConvertToDepartmentsDTO(departments);
        }

        public bool IsDepartmentExist(string id)
        {
            return _db.Departments.FirstOrDefault(x => x.Guid == id) != null;
        }

        private List<Department> ConvertToDepartmentsDTO(List<Model.Department> departments)
        {
            List<Department> result = new List<Department>();
            var processedDepartments = new List<int>();
            foreach (var department in departments)
            {
                if (department.MainDepartment == null)
                {
                    result.Add(CreateDepartment(department, departments, ref processedDepartments));
                }
            }
            return result;
        }
        private Department CreateDepartment(Model.Department department, List<Model.Department> departments, ref List<int> processedDepartments)
        {
            return new Department()
            {
                Guid = department.Guid,
                Name = department.Name,
                Subdepartments = CreateDepartments(department.Id, departments, ref processedDepartments)
            };
        }

        private List<Department> CreateDepartments(int departmentId, List<Model.Department> departments, ref List<int> processedDepartments)
        {
            List<Department> result = new List<Department>();
            processedDepartments.Add(departmentId);
            foreach (var department in departments)
            {
                if (department.MainDepartment != null &&
                    department.MainDepartment.Id == departmentId &&
                    !processedDepartments.Contains(department.Id))
                {
                    result.Add(CreateDepartment(department, departments, ref processedDepartments));
                }
            }
            return result;
        }
    }
}
