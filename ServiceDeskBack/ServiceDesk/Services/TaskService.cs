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
        private readonly string _baseUrl = "http://servicedesk01-001-site1.dtempurl.com/";
        public TaskService(DBContext context)
        {
            _context = context;
        }
        public List<Task> GetTasks()
        {
            return _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).ToList().ConvertAll<Task>(ConvertToTaskDTO);
        }
        public Task GetTaskById(string id)
        {
            var task = _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).FirstOrDefault(x => x.Guid == id);
            if (task == null)
            {
                throw new Exception("Unknown task id");
            }
            return ConvertToTaskDTO(task);
        }
        public List<Task> GetTasksByAssignedId(string assignedId)
        {
            return _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).Where(e => e.Assigned.Guid == assignedId).ToList().ConvertAll<Task>(ConvertToTaskDTO);
        }
        public List<Task> GetTasksByCreatedId(string createdId)
        {
            return _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).Where(e => e.Created.Guid == createdId).ToList().ConvertAll<Task>(ConvertToTaskDTO);
        }

        public List<Task> GetTasksByDepartmentId(string departmentId)
        {
            return _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).Where(e => e.Department.Guid == departmentId).ToList().ConvertAll<Task>(ConvertToTaskDTO);
        }

        private Task ConvertToTaskDTO(Model.Task task)
        {
            var result = new Task()
            {
                Guid = task.Guid,
                Title = task.Title,
                Description = task.Description,
                CreatedDate = DateTime.ParseExact(task.CreatedDate, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture),
                FinishDate = DateTime.ParseExact(task.FinishDate, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture),
                State = (State)task.State,
                Created = new Employee()
                {
                    Guid = task.Created.Guid,
                    Name = task.Created.Name,
                    PhotoPath = _baseUrl + task.Created.PhotoPath
                },
            };
            if (task.Assigned != null)
            {
                result.Assigned = new Employee()
                {
                    Guid = task.Assigned.Guid,
                    Name = task.Assigned.Name,
                    PhotoPath = _baseUrl + task.Assigned.PhotoPath
                };
            }
            if (task.Department != null)
            {
                result.Department = new Department()
                {
                    Guid = task.Department.Guid,
                    Name = task.Department.Name
                };
            }
            return result;
        }

        public void EditTask(Task task)
        {
            var taskModel = _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).FirstOrDefault(x => x.Guid == task.Guid);
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
                var assigned = task.Assigned != null ? _context.Employees.Include(x => x.Department).FirstOrDefault(x => x.Guid == task.Assigned.Guid) : null;
                if (assigned != null)
                {
                    taskModel.Assigned = assigned;
                    taskModel.Department = assigned.Department != null ? _context.Departments.FirstOrDefault(x => x.Id == assigned.Department.Id) : null;
                }
                else if (task.Department != null)
                {
                    taskModel.Department = _context.Departments.FirstOrDefault(x => x.Guid == task.Department.Guid);
                    taskModel.Assigned = null;
                }
                ValidateAssigned(taskModel);
            }
            else
            {
                var newTask = new Model.Task();
                newTask.Guid = Guid.NewGuid().ToString();
                newTask.Title = task.Title != null ? task.Title : "";
                newTask.Description = task.Description != null ? task.Description : "";
                newTask.CreatedDate = DateTime.Now.ToString("yyyyMMddTHH:mm:ssZ");
                newTask.FinishDate = task.FinishDate.ToString("yyyyMMddTHH:mm:ssZ");
                newTask.State = (Model.State)task.State;
                newTask.Created = task.Created != null ? _context.Employees.FirstOrDefault(x => x.Guid == task.Created.Guid) : throw new Exception("Creator must be specified");
                if (newTask.Created == null)
                {
                    throw new Exception("Creator does not exist");
                }
                var assigned = task.Assigned != null ? _context.Employees.Include(x => x.Department).FirstOrDefault(x => x.Guid == task.Assigned.Guid) : null;
                if (assigned != null)
                {
                    newTask.Assigned = assigned;
                    newTask.Department = assigned.Department != null ? _context.Departments.FirstOrDefault(x => x.Id == assigned.Department.Id) : null;
                }
                else if(task.Department != null)
                {
                    newTask.Department = _context.Departments.FirstOrDefault(x => x.Guid == task.Department.Guid);
                    newTask.Assigned = null;
                }
                ValidateAssigned(newTask);
                _context.Add(newTask);
            }
            _context.SaveChanges();
        }

        private void ValidateAssigned(Model.Task task)
        {
            if (task.Department == null && task.Assigned == null)
            {
                throw new Exception("Department or assigned must be specified");
            }
        }

        public void EditTaskState(TaskState taskState)
        {
            var taskModel = _context.Tasks.FirstOrDefault(x => x.Guid == taskState.Guid);
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
