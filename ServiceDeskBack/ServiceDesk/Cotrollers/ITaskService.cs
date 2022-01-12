using System.Collections.Generic;
using ServiceDesk.DTO.Task;

namespace ServiceDesk.Cotrollers
{
    public interface ITaskService
    {
        public System.Threading.Tasks.Task<List<Task>> GetTasksAsync();
        public System.Threading.Tasks.Task<Task> GetTaskByIdAsync(string id);
        public System.Threading.Tasks.Task<List<Task>> GetTasksByAssignedIdAsync(string assignedId);
        public System.Threading.Tasks.Task<List<Task>> GetTasksByCreatedIdAsync(string createdId);
        public System.Threading.Tasks.Task<List<Task>> GetTasksByDepartmentIdAsync(string departmentId);
        public System.Threading.Tasks.Task EditTaskAsync(Task task);
        public void EditTaskState(TaskState taskState);
    }
}
