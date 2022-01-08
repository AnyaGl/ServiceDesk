using Microsoft.AspNetCore.Mvc;
using ServiceDesk.DTO.Task;
using System;

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
        public IActionResult GetTasks()
        {
            return Ok(_taskService.GetTasks());
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(string id)
        {
            try
            {
                var task = _taskService.GetTaskById(id);
                return Ok(task);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("assigned/{assignedId}")]
        public IActionResult GetTasksByAssignedId(string assignedId)
        {
            try
            {
                var tasks = _taskService.GetTasksByAssignedId(assignedId);
                return Ok(tasks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("creator/{createdId}")]
        public IActionResult GetTasksByCretedId(string createdId)
        {
            try
            {
                var tasks = _taskService.GetTasksByCreatedId(createdId);
                return Ok(tasks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("department/{departmentId}")]
        public IActionResult GetTasksByDepartmentId(string departmentId)
        {
            try
            {
                var tasks = _taskService.GetTasksByDepartmentId(departmentId);
                return Ok(tasks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddObject(Task task)
        {
            try
            {
                _taskService.EditTask(task);
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
