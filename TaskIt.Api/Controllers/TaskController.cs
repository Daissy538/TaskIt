using Microsoft.AspNetCore.Mvc;
using TaskIt.Api.Dtos.Input;
using TaskIt.Api.Dtos.Output;
using TaskIt.Core;
using TaskIt.Core.Entities;
using TaskIt.Core.RepositoryInterfaces;
using TaskIt.Core.Request;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IntegrationTests
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController: Controller
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TaskItemDto>> PostTaskAsync(TaskCreateRequestDto taskCreateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var data = new CreateTaskRequest() { 
                Title= taskCreateRequest.Title, 
                EndDate = taskCreateRequest.EndDate 
            };

            var taskItem = await this.taskService.CreateTaskAsync(data);

            var response = new TaskItemDto { 
                Id = taskItem.Id, 
                Title = taskItem.Title, 
                EndDate = taskItem.EndDate 
            };

            return Created($"Task/{response.Id}", response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            var isDeleted = await this.taskService.DeleteTaskAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskItemDto>> GetTaskAsync(Guid id)
        {
            var taskItem = await this.taskService.GetByIdAsync(id);

            if(taskItem == default)
            {
                return NotFound();
            }

            var response = new TaskItemDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                EndDate = taskItem.EndDate
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskItemDto>> GetAllTasksAsync()
        {
            var tasks = await taskService.GetAllAsync();

           var response = tasks.Select(taskItem => new TaskItemDto
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                EndDate = taskItem.EndDate
            });


            return Ok(response);
        }
    }
}