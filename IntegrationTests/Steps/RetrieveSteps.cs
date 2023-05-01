using FluentAssertions;
using IntegrationTests.BaseTests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using TaskIt.Api.Dtos.Output;

namespace IntegrationTests.Steps
{
    public class RetrieveSteps : TestBase, IClassFixture<WebApplicationFactory<Program>>, IAsyncDisposable
    {
        public RetrieveSteps(WebApplicationFactory<Program> factory): base(factory)
        {
        }

        public async ValueTask DisposeAsync()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Fact]
        public async void Retrieve_A_Step()
        {
            var endDate = "4040-01-25T20:11:42Z";
            var createdTaskId = await PostNewTask(DateTime.Parse(endDate), TASK_TITLE);
            var createdStepId = await PostNewStep(createdTaskId);

            var responseRequest = await _client.GetAsync($"{STEP_URL}/{createdStepId}");
            var responseBody = await responseRequest.Content.ReadAsStringAsync();
            var stepItem = JsonSerializer.Deserialize<StepItemDto>(responseBody);

            stepItem.Should().NotBeNull();
            stepItem.Title.Should().BeEquivalentTo(STEP_TITLE);
            stepItem.Description.Should().BeEquivalentTo(STEP_DESCRIPTION);
        }
    }
}
