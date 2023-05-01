using IntegrationTests.requestBuilders;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using TaskIt.Api.Dtos.Input;
using TaskIt.Api.Dtos.Output;

namespace IntegrationTests.BaseTests
{
    public abstract class TestBase
    {
        protected const string TASK_TITLE = "Katten bak schoonmaken";
        protected const string TASK_URL = "Task";
        protected const string STEP_URL = "Step";
        protected const string STEP_TITLE = "Klonten scheppen";
        protected const string STEP_DESCRIPTION = "Test omschrijving";

        protected readonly WebApplicationFactory<Program> _factory;
        protected readonly HttpClient _client;

        public TestBase(WebApplicationFactory<Program>? factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        protected async Task PostMultipleNewTasks(DateTime endDate, string title, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                await PostNewTask(endDate, title);
            }
        }

        protected async Task<Guid> PostNewTask(DateTime endDate, string title)
        {
            var taskCreateData = new TaskCreateRequestBuilder()
                .WithTitle(title)
                .WithEndDate(endDate)
                .Create();

            using StringContent httpContentString = new HttpStringContentBuilder<TaskCreateRequestDto>()
                .WithMediaTypeAplicationJson()
                .WithEndocdingUTF8()
                .WithContent(taskCreateData)
            .Create();

            var responseCreateRequest = await _client.PostAsync(TASK_URL, httpContentString);

            var responseBody = await responseCreateRequest.Content.ReadAsStringAsync();
            var createdTaskItem = JsonSerializer.Deserialize<TaskItemDto>(responseBody);

            return createdTaskItem.Id;
        }

        protected async Task<Guid> PostNewStep(Guid createdTaskId)
        {
            var stepCreateData = new StepCreateRequestBuilder()
                         .WithTitle(STEP_TITLE)
                         .WithDescription(STEP_DESCRIPTION)
                         .WithTask(createdTaskId)
                         .Create();


            using StringContent httpContentString = new HttpStringContentBuilder<StepCreatedRequestDto>()
                                                .WithMediaTypeAplicationJson()
                                                .WithEndocdingUTF8()
                                                .WithContent(stepCreateData)
                                                .Create();

            var responseCreateRequest = await _client.PostAsync(STEP_URL, httpContentString);

            var responseBody = await responseCreateRequest.Content.ReadAsStringAsync();
            var createdStepItem = JsonSerializer.Deserialize<StepItemDto>(responseBody);

            return createdStepItem.Id;
        }
    }
}
