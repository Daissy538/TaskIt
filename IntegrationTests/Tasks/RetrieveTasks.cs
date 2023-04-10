﻿using FluentAssertions;
using IntegrationTests.requestBuilders;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using TaskIt.Api.Dtos.Input;
using TaskIt.Api.Dtos.Output;

namespace IntegrationTests.Tasks
{
    public class RetrieveTasks : IClassFixture<WebApplicationFactory<Program>>, IAsyncDisposable
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";

        private const string TASK_URL = "Task";

        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public RetrieveTasks(WebApplicationFactory<Program> factory)
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
        public async Task Retrieve_Task()
        {
            //ARANGE
            var endDate = "4040-01-25T20:11:42Z";

            var createdTaskId = await PostNewTask(DateTime.Parse(endDate), TASK_TITLE);

            //ACT
            var repsonseAfterAct = await _client.GetAsync($"{TASK_URL}/{createdTaskId}");

            //ASSERT
            var responseStringAfterAct = await repsonseAfterAct.Content.ReadAsStringAsync();
            var taskItemAfterAct = JsonSerializer.Deserialize<TaskItemDto>(responseStringAfterAct);

            Assert.NotNull(taskItemAfterAct);
            Assert.Equal(TASK_TITLE, taskItemAfterAct.Title);
            Assert.Equal(DateTime.Parse(endDate), taskItemAfterAct.EndDate);
        }

        [Fact]
        public async Task Retrieve_All_Tasks()
        {
            //Arange
            var endDate = "4040-01-25T20:11:42Z";
            var amountOfCreatedTasks = 3;

            var httpClient = _factory.CreateClient();

            await PostMultipleNewTasks(DateTime.Parse(endDate), TASK_TITLE, amountOfCreatedTasks);

            //Act
            var repsonseAfterAct = await httpClient.GetAsync($"{TASK_URL}/All");


            //Assert
            var responseStringAfterAct = await repsonseAfterAct.Content.ReadAsStringAsync();
            var taskItemsAfterAct = JsonSerializer.Deserialize<List<TaskItemDto>>(responseStringAfterAct);

            taskItemsAfterAct.Should().NotBeNull();
            taskItemsAfterAct.Count.Should().BeGreaterThanOrEqualTo(amountOfCreatedTasks);
        }

        private async Task PostMultipleNewTasks(DateTime endDate, string title, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                await PostNewTask(endDate, title);
            }
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
