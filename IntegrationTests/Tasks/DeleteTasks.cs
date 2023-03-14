using IntegrationTests.requestBuilders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using TaskIt.Api.Dtos.Input;
using TaskIt.Api.Dtos.Output;

namespace IntegrationTests.Tasks
{
    public class DeleteTasks : IClassFixture<WebApplicationFactory<Program>>
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";
        private const string TASK_URL = "Task";

        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient client;

        public DeleteTasks(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            client = _factory.CreateClient();
        }

        [Fact]
        public async Task Delete_Task_By_Id()
        {
            //Arrange
            var endDate = "4040-01-25T20:11:42Z";

            var createdTaskId = await PostNewTask(DateTime.Parse(endDate), TASK_TITLE);

            //Act
            var response = await client.DeleteAsync($"{TASK_URL}/{createdTaskId}");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Can_Not_Delete_Task_That_Does_Not_Exist()
        {
            //Arrange
            var endDate = "4040-01-25T20:11:42Z";

            var createdTaskId = await PostNewTask(DateTime.Parse(endDate), TASK_TITLE);

            //Act
            var response = await client.DeleteAsync($"{TASK_URL}/{Guid.NewGuid()}");

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
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

            var responseCreateRequest = await client.PostAsync(TASK_URL, httpContentString);

            var responseBody = await responseCreateRequest.Content.ReadAsStringAsync();
            var createdTaskItem = JsonSerializer.Deserialize<TaskItemDto>(responseBody);
            return createdTaskItem.Id;
        }
    }
}
