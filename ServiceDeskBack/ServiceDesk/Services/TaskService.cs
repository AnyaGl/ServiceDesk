using ServiceDesk.Cotrollers;
using ServiceDesk.Model;
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
            return _context.Tasks.ToList();
        }
        public Task GetTaskById(int id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                throw new Exception("Unknown task id");
            }
            return task;
        }
        public List<Task> GetTasksByAssignedId(int assignedId)
        {
            return _context.Tasks.Where(e => e.AssignedId == assignedId).ToList();
        }        
        public List<Task> GetTasksByCreatedId(int createdId)
        {
            return _context.Tasks.Where(e => e.CreatedId == createdId).ToList();
        }
    }
}
