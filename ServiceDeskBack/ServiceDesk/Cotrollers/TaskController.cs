using Microsoft.AspNetCore.Mvc;
using ServiceDesk.DTO.Task;
using System;
using System.Linq;

namespace ServiceDesk.Cotrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("all")]
        public async System.Threading.Tasks.Task<IActionResult> GetTasksAsync()
        {
            return Ok(await _taskService.GetTasksAsync());
        }

        [HttpGet("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> GetTaskByIdAsync(string id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                return Ok(task);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("assigned/{assignedId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetTasksByAssignedIdAsync(string assignedId)
        {
            try
            {
                var tasks = await _taskService.GetTasksByAssignedIdAsync(assignedId);
                return Ok(tasks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("creator/{createdId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetTasksByCretedIdAsync(string createdId)
        {
            try
            {
                var tasks = await _taskService.GetTasksByCreatedIdAsync(createdId);
                return Ok(tasks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("department/{departmentId}")]
        public async System.Threading.Tasks.Task<IActionResult> GetTasksByDepartmentIdAsync(string departmentId)
        {
            try
            {
                var tasks = await _taskService.GetTasksByDepartmentIdAsync(departmentId);
                return Ok(tasks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("add")]
        public async System.Threading.Tasks.Task<IActionResult> AddObjectAsync(Task task)
        {
            try
            {
                await _taskService.EditTaskAsync(task);
                return Ok(new TaskResult
                {
                    ErrorCode = 0
                });
            }
            catch (Exception)
            {
                return Ok(new TaskResult
                {
                    ErrorCode = 1
                });
            }
        }


        [HttpPost("state")]
        public IActionResult EditTaskState(TaskState taskState)
        {
            try
            {
                _taskService.EditTaskState(taskState);
                return Ok(new TaskResult
                {
                    ErrorCode = 0
                });
            }
            catch (Exception)
            {
                return Ok(new TaskResult
                {
                    ErrorCode = 1
                });
            }
        }
    }
    public class TaskResult
    {
        public int ErrorCode { get; set; }
    }
}
