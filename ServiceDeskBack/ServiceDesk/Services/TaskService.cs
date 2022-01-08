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
        public TaskService(DBContext context)
        {
            _context = context;
        }
        public List<Task> GetTasks()
        {
            return _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).ToList().ConvertAll<Task>(ConvertToTaskDTO);
        }
        public Task GetTaskById(int id)
        {
            var task = _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                throw new Exception("Unknown task id");
            }
            return ConvertToTaskDTO(task);
        }
        public List<Task> GetTasksByAssignedId(int assignedId)
        {
            return _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).Where(e => e.Assigned.Id == assignedId).ToList().ConvertAll<Task>(ConvertToTaskDTO);
        }
        public List<Task> GetTasksByCreatedId(int createdId)
        {
            return _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).Where(e => e.Created.Id == createdId).ToList().ConvertAll<Task>(ConvertToTaskDTO);
        }

        private Task ConvertToTaskDTO(Model.Task task)
        {
            var result = new Task()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedDate = DateTime.ParseExact(task.CreatedDate, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture),
                FinishDate = DateTime.ParseExact(task.FinishDate, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture),
                State = (State)task.State,
                Created = new Employee()
                {
                    Id = task.Created.Id,
                    Name = task.Created.Name
                },
            };
            if (task.Assigned != null)
            {
                result.Assigned = new Employee()
                {
                    Id = task.Assigned.Id,
                    Name = task.Assigned.Name
                };
            }
            if (task.Department != null)
            {
                result.Department = new Department()
                {
                    Id = task.Department.Id,
                    Name = task.Department.Name
                };
            }
            return result;
        }

        public void EditTask(Task task)
        {
            var taskModel = _context.Tasks.Include(x => x.Assigned).Include(x => x.Created).Include(x => x.Department).FirstOrDefault(x => x.Id == task.Id);
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
                var assigned = task.Assigned != null ? _context.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == task.Assigned.Id) : null;
                if (assigned != null)
                {
                    taskModel.Assigned = assigned;
                    taskModel.Department = assigned.Department != null ? _context.Departments.FirstOrDefault(x => x.Id == assigned.Department.Id) : null;
                }
                else if (task.Department != null)
                {
                    taskModel.Department = _context.Departments.FirstOrDefault(x => x.Id == task.Department.Id);
                }
            }
            else
            {
                var newTask = new Model.Task();
                newTask.Title = task.Title != null ? task.Title : "";
                newTask.Description = task.Description != null ? task.Description : "";
                newTask.CreatedDate = DateTime.Now.ToString("yyyyMMddTHH:mm:ssZ");
                newTask.FinishDate = task.FinishDate.ToString("yyyyMMddTHH:mm:ssZ");
                newTask.State = (Model.State)task.State;
                newTask.Created = task.Created != null ? _context.Employees.FirstOrDefault(x => x.Id == task.Created.Id) : throw new Exception("Creator must be specified");
                if (newTask.Created == null)
                {
                    throw new Exception("Creator does not exist");
                }
                var assigned = task.Assigned != null ? _context.Employees.Include(x => x.Department).FirstOrDefault(x => x.Id == task.Assigned.Id) : null;
                if (assigned != null)
                {
                    newTask.Assigned = assigned;
                    newTask.Department = assigned.Department != null ? _context.Departments.FirstOrDefault(x => x.Id == assigned.Department.Id) : null;
                }
                else if(task.Department != null)
                {
                    newTask.Department = _context.Departments.FirstOrDefault(x => x.Id == task.Department.Id);
                }
                _context.Add(newTask);
            }
            _context.SaveChanges();
        }

        public void EditTaskState(TaskState taskState)
        {
            var taskModel = _context.Tasks.FirstOrDefault(x => x.Id == taskState.Id);
            if (taskModel != null)
            {
                taskModel.State = (Model.State)taskState.State;
            }
            else
            {
                throw new Exception($"Task with id={taskState.Id} does not exist");
            }
            _context.SaveChanges();
        }
    }
}
