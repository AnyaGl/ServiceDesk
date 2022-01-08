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

        [HttpGet("get-tasks")]
        public IActionResult GetTasks()
        {
            return Ok(_taskService.GetTasks());
        }

        [HttpGet("get-task-by-id/{id}")]
        public IActionResult GetTaskById(int id)
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
        
        [HttpGet("get-tasks-by-assigned-id/{assignedId}")]
        public IActionResult GetTasksByAssignedId(int assignedId)
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

        [HttpGet("get-tasks-by-created-id/{createdId}")]
        public IActionResult GetTasksByCretedId(int createdId)
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

        [HttpGet("get-tasks-by-department-id/{departmentId}")]
        public IActionResult GetTasksByDepartmentId(int departmentId)
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
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost()]
        public IActionResult EditTaskState(TaskState taskState)
        {
            try
            {
                _taskService.EditTaskState(taskState);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
