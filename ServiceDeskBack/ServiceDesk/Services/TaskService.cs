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
            return new Task()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedDate = DateTime.ParseExact(task.CreatedDate, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture),
                FinishDate = DateTime.ParseExact(task.FinishDate, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture),
                State = (State)task.State,
                Assigned = new Employee()
                {
                    Id = task.Assigned.Id,
                    Name = task.Assigned.Name
                },
                Created = new Employee()
                {
                    Id = task.Created.Id,
                    Name = task.Created.Name
                },
                Department = new Department()
                {
                    Id = task.Department.Id,
                    Name = task.Department.Name
                }
            };
        }
    }
}
