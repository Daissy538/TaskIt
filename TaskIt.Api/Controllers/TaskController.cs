using Microsoft.AspNetCore.Mvc;
using TaskIt.Core;
using TaskIt.Core.Request;

namespace IntegrationTests
{
    [ApiController]
    [Route("Api/[controller]")]
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
        public async Task<IActionResult> Create(TaskCreateRequestDto taskCreateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(StatusCodes.Status400BadRequest);
            }

            var data = new CreateTaskRequest() { 
                Title= taskCreateRequest.Title, 
                EndDate = taskCreateRequest.EndDate 
            };

            var taskItem = this.taskService.CreateTask(data);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}