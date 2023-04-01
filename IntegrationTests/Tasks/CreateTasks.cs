using IntegrationTests.requestBuilders;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskIt.Api.Dtos.Input;

namespace IntegrationTests.Tasks
{
    public class CreateTasks : IClassFixture<WebApplicationFactory<Program>>
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";

        private const string TASK_URL = "Task";

        private readonly WebApplicationFactory<Program> _factory;

        private readonly HttpClient _client;

        public CreateTasks(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Theory]
        [InlineData(TASK_TITLE, "4040-01-25T20:11:42Z")]
        [InlineData(TASK_TITLE, null)]
        public async void Add_A_Task(string title, string? dateTime)
        {
            //Arrange
            var TaskCreateData = new TaskCreateRequestBuilder()
                .WithTitle(title)
                .WithEndDate(dateTime != null ? DateTime.Parse(dateTime) : null)
                .Create();

            using StringContent request = new HttpStringContentBuilder<TaskCreateRequestDto>()
                .WithMediaTypeAplicationJson()
                .WithEndocdingUTF8()
                .WithContent(TaskCreateData)
                .Create();

            //Act
            var response = await _client.PostAsync(TASK_URL, request);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void Can_Not_Add_A_Task_Without_Title(string title)
        {
            //ARANGE
            var taskCreateData = new TaskCreateRequestBuilder()
                .WithTitle(title)
                .Create();

            using StringContent request = new HttpStringContentBuilder<TaskCreateRequestDto>()
                .WithMediaTypeAplicationJson()
                .WithEndocdingUTF8()
                .WithContent(taskCreateData)
                .Create();

            //Act
            var response = await _client.PostAsync(TASK_URL, request);

            //ASSERT
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}