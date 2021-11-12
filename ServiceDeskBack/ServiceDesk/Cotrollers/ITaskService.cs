using System.Collections.Generic;
using ServiceDesk.Model;

namespace ServiceDesk.Cotrollers
{
    public interface ITaskService
    {
        public List<Task> GetTasks();
        public Task GetTaskById(int id);
        public List<Task> GetTasksByAssignedId(int assignedId);
        public List<Task> GetTasksByCreatedId(int createdId);
    }
}
