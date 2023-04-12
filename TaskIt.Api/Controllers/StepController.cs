using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        public async Task<ActionResult<TaskItemDto>> PostStepAsync(StepCreatedRequestDto stepCreateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }

                return(Ok());
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }

        }
    }
}