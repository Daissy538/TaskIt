using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskIt.Api.Dtos.Input;
using TaskIt.Api.Dtos.Output;
using TaskIt.Core;
using TaskIt.Core.Entities;
using TaskIt.Core.Request;

namespace IntegrationTests
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController: Controller
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskService, ILogger<TaskController> logger)
        {
            this._taskService = taskService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TaskItemDto>> PostTaskAsync(TaskCreateRequestDto taskCreateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }

                var data = new CreateTaskRequest()
                {
                    Title = taskCreateRequest.Title,
                    EndDate = taskCreateRequest.EndDate
                };

                var taskItem = await this._taskService.CreateTaskAsync(data);

                var response = new TaskItemDto
                {
                    Id = taskItem.Id,
                    Title = taskItem.Title,
                    EndDate = taskItem.EndDate
                };

                return Created($"Task/{response.Id}", response);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            try
            {
                var isDeleted = await this._taskService.DeleteTaskAsync(id);

                if (!isDeleted)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskItemDto>> GetTaskAsync(Guid id)
        {
            try
            {
                var taskItem = await this._taskService.GetByIdAsync(id);

                if (taskItem == default)
                {
                    return NotFound();
                }

                var response = new TaskItemDto
                {
                    Id = taskItem.Id,
                    Title = taskItem.Title,
                    EndDate = taskItem.EndDate,
                    Steps = taskItem.Steps.Select(s =>
                    {
                        return new StepItemDto
                        {
                            Id = s.Id,
                            Description = s.Description,
                            Title = s.Title
                        };
                    }).ToList()
                };

                return Ok(response);
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TaskItemDto>> GetAllTasksAsync()
        {
            try
            {
                var tasks = await _taskService.GetAllAsync();

                var response = tasks.Select(taskItem => new TaskItemDto
                {
                    Id = taskItem.Id,
                    Title = taskItem.Title,
                    EndDate = taskItem.EndDate
                });


                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }
    }
}