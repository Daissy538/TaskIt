using FluentAssertions;
using IntegrationTests.BaseTests;
using IntegrationTests.requestBuilders;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using TaskIt.Api.Dtos.Input;
using TaskIt.Api.Dtos.Output;

namespace IntegrationTests.Steps
{
    public class AddTaskSteps : TestBase, IClassFixture<WebApplicationFactory<Program>>, IAsyncDisposable
    {
        public AddTaskSteps(WebApplicationFactory<Program> factory): base(factory)
        {
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

            var stepCreateData = new StepCreateRequestBuilder()
                                    .WithTitle("Klonten scheppen")
                                    .WithDescription("Test omschrijving")
                                    .WithTask(createdTaskId)
                                    .Create();

            using StringContent httpContentString = new HttpStringContentBuilder<StepCreatedRequestDto>()
                                                            .WithMediaTypeAplicationJson()
                                                            .WithEndocdingUTF8()
                                                            .WithContent(stepCreateData)
                                                            .Create();

            var responseCreateRequest = await _client.PostAsync(STEP_URL, httpContentString);

            var responseBody = await responseCreateRequest.Content.ReadAsStringAsync();
            var createdStepItem = JsonSerializer.Deserialize<TaskItemDto>(responseBody);

            responseCreateRequest.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
