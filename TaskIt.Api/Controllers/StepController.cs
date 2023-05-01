using Microsoft.AspNetCore.Mvc;
using TaskIt.Api.Dtos.Input;
using TaskIt.Api.Dtos.Output;
using TaskIt.Core;
using TaskIt.Core.Request;

namespace IntegrationTests
{
    [ApiController]
    [Route("[controller]")]
    public class StepController: Controller
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        public StepController(ITaskService taskService, ILogger<TaskController> logger)
        {
            this._taskService = taskService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StepItemDto>> PostStepAsync(StepCreatedRequestDto stepCreateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }

                var stepRequest = new CreateStepRequest()
                {
                    TaskId = stepCreateRequest.TaskId,
                    Description = stepCreateRequest.Description,
                    Title = stepCreateRequest.Title
                };

                var step = await _taskService.AddStepToTaskAsync(stepRequest);

                var response = new StepItemDto
                {
                    Id = step.Id,
                    Description = step.Description,
                    Title = step.Title
                };
                
                return Created($"Step/{response.Id}", response);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<StepItemDto>>> GetStepAsync(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }

                var steps = await _taskService.GetStepByIdAsync(id);

                if(steps == default)
                {
                    return NoContent();
                }

                return Ok(steps);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

    }
}