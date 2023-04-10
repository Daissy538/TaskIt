using FluentAssertions;
using IntegrationTests.requestBuilders;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using TaskIt.Api.Dtos.Input;
using TaskIt.Api.Dtos.Output;

namespace IntegrationTests.Tasks
{
    public class AddTaskSteps : IClassFixture<WebApplicationFactory<Program>>, IAsyncDisposable
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";
        private const string TASK_URL = "Task";

        private readonly WebApplicationFactory<Program> _factory;

        private readonly HttpClient _client;

        public AddTaskSteps(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        public async ValueTask DisposeAsync()
        {
            _client.Dispose();
            _factory.Dispose();
        }


        [Fact]
        public async void Add_A_Step_To_A_Task()
        {
            var endDate = "4040-01-25T20:11:42Z";
            var createdTaskId = await PostNewTask(DateTime.Parse(endDate), TASK_TITLE);

            createdTaskId.Should().NotBeEmpty(); 
        }

        private async Task<Guid> PostNewTask(DateTime endDate, string title)
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
    }
}
