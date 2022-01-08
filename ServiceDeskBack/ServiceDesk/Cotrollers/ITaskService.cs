using System.Collections.Generic;
using ServiceDesk.DTO.Task;

namespace ServiceDesk.Cotrollers
{
    public interface ITaskService
    {
        public List<Task> GetTasks();
        public Task GetTaskById(int id);
        public List<Task> GetTasksByAssignedId(int assignedId);
        public List<Task> GetTasksByCreatedId(int createdId);
        public void EditTask(Task task);
        public void EditTaskState(TaskState taskState);
    }
}
