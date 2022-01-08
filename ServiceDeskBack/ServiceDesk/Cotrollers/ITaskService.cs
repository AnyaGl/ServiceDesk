using System.Collections.Generic;
using ServiceDesk.DTO.Task;

namespace ServiceDesk.Cotrollers
{
    public interface ITaskService
    {
        public List<Task> GetTasks();
        public Task GetTaskById(string id);
        public List<Task> GetTasksByAssignedId(string assignedId);
        public List<Task> GetTasksByCreatedId(string createdId);
        public List<Task> GetTasksByDepartmentId(string departmentId);
        public void EditTask(Task task);
        public void EditTaskState(TaskState taskState);
    }
}
