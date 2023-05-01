using FluentAssertions;
using IntegrationTests.BaseTests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using TaskIt.Api.Dtos.Output;

namespace IntegrationTests.Tasks
{
    public class RetrieveTasks : TestBase, IClassFixture<WebApplicationFactory<Program>>, IAsyncDisposable
    {
        public RetrieveTasks(WebApplicationFactory<Program> factory): base(factory)
        {
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
            taskItemsAfterAct.Should().HaveCountGreaterThan(amountOfCreatedTasks);
        }

        [Fact]
        public async Task Retrieve_Task_With_Steps()
        {
            //ARANGE
            var endDate = "4040-01-25T20:11:42Z";
            var amountOfSteps = 2;

            var createdTaskId = await PostNewTask(DateTime.Parse(endDate), TASK_TITLE);

            for(int i = 1; i <= amountOfSteps; i++)
            {
                await PostNewStep(createdTaskId);
            }

            //ACT
            var repsonseAfterAct = await _client.GetAsync($"{TASK_URL}/{createdTaskId}");

            //ASSERT
            var responseStringAfterAct = await repsonseAfterAct.Content.ReadAsStringAsync();
            var taskItemAfterAct = JsonSerializer.Deserialize<TaskItemDto>(responseStringAfterAct);

            taskItemAfterAct.Should().NotBeNull();            
            taskItemAfterAct.Title.Should().Be(TASK_TITLE);
            taskItemAfterAct.EndDate.Should().Be(DateTime.Parse(endDate));
            taskItemAfterAct.Steps.Should().HaveCount(amountOfSteps);
        }
    }
}
