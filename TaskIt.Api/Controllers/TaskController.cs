using Microsoft.AspNetCore.Mvc;
using TaskIt.Api.Dtos.Input;
using TaskIt.Api.Dtos.Output;
using TaskIt.Core;
using TaskIt.Core.Entities;
using TaskIt.Core.RepositoryInterfaces;
using TaskIt.Core.Request;

namespace IntegrationTests
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController: Controller
    {
        private readonly ITaskService taskService;
        private readonly ITaskRepository taskRepository;

        public TaskController(ITaskService taskService, ITaskRepository taskRepository)
        {
            this.taskService = taskService;
            this.taskRepository = taskRepository;
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

            var taskItem = this.taskService.CreateTask(data);

            taskRepository.Add(taskItem);

            var response = new TaskItemDto { 
                Id = taskItem.Id, 
                Title = taskItem.Title, 
                EndDate = taskItem.EndDate 
            };

            return Created($"Task/{response.Id}", response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskItemDto>> GetTaskAsync(Guid id)
        {
            var taskItem = this.taskRepository.GetById(id);

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
            var tasks = this.taskRepository.GetAll();

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