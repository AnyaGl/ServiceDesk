using Microsoft.EntityFrameworkCore;
using ServiceDesk.Cotrollers;
using ServiceDesk.DTO.Task;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceDesk.Services
{
    public class TaskService : ITaskService
    {
        private readonly DBContext _context;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        public TaskService(DBContext context, IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _context = context;
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public async System.Threading.Tasks.Task<List<Task>> GetTasksAsync()
        {
            return await ConvertToTasksDTOAsync(_context.Tasks.ToList());
        }


        public async System.Threading.Tasks.Task<Task> GetTaskByIdAsync(string id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                throw new Exception("Unknown task id");
            }
            return await ConvertToTaskDTOAsync(task);
        }
        public async System.Threading.Tasks.Task<List<Task>> GetTasksByAssignedIdAsync(string assignedId)
        {
            return await ConvertToTasksDTOAsync(_context.Tasks.Where(e => e.AssignedId == assignedId).ToList());
        }
        public async System.Threading.Tasks.Task<List<Task>> GetTasksByCreatedIdAsync(string createdId)
        {
            return await ConvertToTasksDTOAsync(_context.Tasks.Where(e => e.CreatedId == createdId).ToList());
        }

        public async System.Threading.Tasks.Task<List<Task>> GetTasksByDepartmentIdAsync(string departmentId)
        {
            return await ConvertToTasksDTOAsync(_context.Tasks.Where(e => e.DepartmentId == departmentId).ToList());
        }

        private async System.Threading.Tasks.Task<List<Task>> ConvertToTasksDTOAsync(List<Model.Task> tasks)
        {
            List<Task> result = new List<Task>();
            foreach (var task in tasks)
            {
                result.Add(await ConvertToTaskDTOAsync(task));
            }
            return result;
        }

        private async System.Threading.Tasks.Task<Task> ConvertToTaskDTOAsync(Model.Task task)
        {
            var result = new Task()
            {
                Guid = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedDate = DateTime.ParseExact(task.CreatedDate, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture),
                FinishDate = DateTime.ParseExact(task.FinishDate, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture),
                State = (State)task.State,
            };
            var creator = await _employeeService.GetEmployeeById(task.CreatedId);
            result.Created = new Employee()
            {
                Guid = creator.guid,
                Name = creator.name,
                PhotoPath = creator.photoPath
            };
            if (task.AssignedId != null)
            {
                var assigned = await _employeeService.GetEmployeeById(task.AssignedId);
                result.Assigned = new Employee()
                {
                    Guid = assigned.guid,
                    Name = assigned.name,
                    PhotoPath = assigned.photoPath
                };
            }
            if (task.DepartmentId != null)
            {
                var department = await _departmentService.GetDepartmentById(task.DepartmentId);
                result.Department = new Department()
                {
                    Guid = department.guid,
                    Name = department.name
                };
            }
            return result;
        }

        public async System.Threading.Tasks.Task EditTaskAsync(Task task)
        {
            var taskModel = _context.Tasks.FirstOrDefault(x => x.Id == task.Guid);
            if (taskModel != null)
            {
                if (task.Title != null)
                {
                    taskModel.Title = task.Title;
                }
                if (task.Description != null)
                {
                    taskModel.Description = task.Description;
                }
                if (task.FinishDate != DateTime.MinValue)
                {
                    taskModel.FinishDate = task.FinishDate.ToString("yyyyMMddTHH:mm:ssZ");
                }
                if (task.Assigned != null)
                {
                    var assigned = await _employeeService.GetEmployeeById(task.Assigned.Guid);
                    if (assigned != null)
                    {
                        taskModel.AssignedId = assigned.guid;
                        taskModel.DepartmentId = assigned.department != null ? assigned.department.guid : null;
                    }
                }
                else if(task.Department != null)
                {
                    var department = await _departmentService.GetDepartmentById(task.Department.Guid);
                    if (department != null)
                    {
                        taskModel.DepartmentId = department.guid;
                        taskModel.AssignedId = null;
                    }
                }
                ValidateAssigned(taskModel);
            }
            else
            {
                var newTask = new Model.Task();
                newTask.Id = Guid.NewGuid().ToString();
                newTask.Title = task.Title != null ? task.Title : "";
                newTask.Description = task.Description != null ? task.Description : "";
                newTask.CreatedDate = DateTime.Now.ToString("yyyyMMddTHH:mm:ssZ");
                newTask.FinishDate = task.FinishDate.ToString("yyyyMMddTHH:mm:ssZ");
                newTask.State = (Model.State)task.State;
                if (task.Created != null)
                {
                    var created = await _employeeService.GetEmployeeById(task.Created.Guid);
                    if (created != null)
                    {
                        newTask.CreatedId = created.guid;
                    }
                }
                if (newTask.CreatedId == null)
                {
                    throw new Exception("Creator does not exist");
                }
                if (task.Assigned != null)
                {
                    var assigned = await _employeeService.GetEmployeeById(task.Assigned.Guid);
                    if (assigned != null)
                    {
                        newTask.AssignedId = assigned.guid;
                        newTask.DepartmentId = assigned.department != null ? assigned.department.guid : null;
                    }
                }
                else if(task.Department != null)
                {
                    var department = await _departmentService.GetDepartmentById(task.Department.Guid);
                    if (department != null)
                    {
                        newTask.DepartmentId = department.guid;
                        newTask.AssignedId = null;
                    }
                }
                ValidateAssigned(newTask);
                _context.Add(newTask);
            }
            _context.SaveChanges();
        }

        private void ValidateAssigned(Model.Task task)
        {
            if (task.DepartmentId == null && task.AssignedId == null)
            {
                throw new Exception("Department or assigned must be specified");
            }
        }

        public void EditTaskState(TaskState taskState)
        {
            var taskModel = _context.Tasks.FirstOrDefault(x => x.Id == taskState.Guid);
            if ((Model.State)taskState.State > Model.State.Closed || (Model.State)taskState.State < Model.State.Open)
            {
                throw new Exception($"Invalid state");
            }

            if (taskModel != null)
            {
                taskModel.State = (Model.State)taskState.State;
            }
            else
            {
                throw new Exception($"Task with id={taskState.Guid} does not exist");
            }
            _context.SaveChanges();
        }
    }
}
