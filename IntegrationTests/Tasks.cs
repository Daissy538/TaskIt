using IntegrationTests.requestBuilders;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using TaskIt.Api.Dtos.Input;
using TaskIt.Api.Dtos.Output;

namespace IntegrationTests
{
    public class Tasks : IClassFixture<WebApplicationFactory<Program>>
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";

        private const string TASK_URL = "Task";

        private readonly WebApplicationFactory<Program> _factory;

        public Tasks(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(TASK_TITLE, "4040-01-25T20:11:42Z")]
        [InlineData(TASK_TITLE, null)]
        public async void Add_A_Task(string title, string? dateTime)
        {
            //Arrange
            var client = _factory.CreateClient();

            var TaskCreateData = new TaskCreateRequestBuilder()
                .WithTitle(title)
                .WithEndDate(dateTime != null? DateTime.Parse(dateTime): null)
                .Create();

            using StringContent request = new HttpStringContentBuilder<TaskCreateRequestDto>()
                .WithMediaTypeAplicationJson()
                .WithEndocdingUTF8()
                .WithContent(TaskCreateData)
                .Create();

            //Act
            var response = await client.PostAsync(TASK_URL, request);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void Can_Not_Add_A_Task_Without_Title(string title)
        {
            //ARANGE
            var client = _factory.CreateClient();

            var taskCreateData = new TaskCreateRequestBuilder()
                .WithTitle(title)
                .Create();

            using StringContent request = new HttpStringContentBuilder<TaskCreateRequestDto>()
                .WithMediaTypeAplicationJson()
                .WithEndocdingUTF8()
                .WithContent(taskCreateData)
                .Create();

            //Act
            var response = await client.PostAsync(TASK_URL, request);

            //ASSERT
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void Retrieve_Task()
        {
            //ARANGE
            var endDate = "4040-01-25T20:11:42Z";

            var httpClient = _factory.CreateClient();

            var createdTaskId = await this.PostNewTask(httpClient, DateTime.Parse(endDate), TASK_TITLE);

            //ACT
            var repsonseAfterAct = await httpClient.GetAsync($"{TASK_URL}/{createdTaskId}");

            //ASSERT
            var responseStringAfterAct = await repsonseAfterAct.Content.ReadAsStringAsync();
            var taskItemAfterAct = JsonSerializer.Deserialize<TaskItemDto>(responseStringAfterAct);

            Assert.NotNull(taskItemAfterAct);
            Assert.Equal(TASK_TITLE, taskItemAfterAct.Title);
            Assert.Equal(DateTime.Parse(endDate), taskItemAfterAct.EndDate);
        }

        [Fact]
        public async void Retrieve_All()
        {
            //Arange
            var endDate = "4040-01-25T20:11:42Z";
            var amountOfCreatedTasks = 3;

            var httpClient = _factory.CreateClient();

            await this.PostMultipleNewTasks(httpClient, DateTime.Parse(endDate), TASK_TITLE, amountOfCreatedTasks);

            //Act
            var repsonseAfterAct = await httpClient.GetAsync($"{TASK_URL}/All");


            //Assert
            var responseStringAfterAct = await repsonseAfterAct.Content.ReadAsStringAsync();
            var taskItemsAfterAct = JsonSerializer.Deserialize<List<TaskItemDto>>(responseStringAfterAct);

            Assert.NotNull(taskItemsAfterAct);
            Assert.Equal(amountOfCreatedTasks, taskItemsAfterAct.Count());
        }

        private async Task PostMultipleNewTasks(HttpClient httpClient, DateTime endDate, string title, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                await this.PostNewTask(httpClient, endDate, title);
            }
        }

        private async Task<Guid> PostNewTask(HttpClient httpClient, DateTime endDate, string title)
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

            var responseCreateRequest = await httpClient.PostAsync(TASK_URL, httpContentString);

            var responseBody = await responseCreateRequest.Content.ReadAsStringAsync();
            var createdTaskItem = JsonSerializer.Deserialize<TaskItemDto>(responseBody);
            return createdTaskItem.Id;
        }
    }
}