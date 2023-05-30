using TaskIt.Core.Request.Builder;
using TaskIt.Adapter.Fakes;
using TaskIt.Adapter;
using TaskIt.Core.Entities;
using FluentAssertions;
using TaskIt.Core;
using TaskIt.Application;
using TaskIt.Adapter.Fake.Fakes;

namespace UnitTests.Step
{
    public class RetrieveStep : IAsyncLifetime
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";
        private const string STEP_TITLE = "Step 1";
        private const string STEP_DESCRIPTION = "Cleaning the servace";
        private const string CURRENT_DATETIME = "2011-03-21 13:26";

        private StepFakeRepository stepFakeRepository;
        private SystemDateTimeClient systemDateTimeClient;

        private ITaskService taskService;
        private TaskItem task;

        public async Task InitializeAsync()
        {
            stepFakeRepository = new StepFakeRepository();
            systemDateTimeClient = new SystemDateTimeClient(CURRENT_DATETIME);

            taskService = new TaskService(new TaskFakeRepository(), stepFakeRepository, systemDateTimeClient);
            var currentDateTime = DateTime.UtcNow;

            var request = new CreateTaskRequestBuilder()
            .WithTitle(TASK_TITLE)
            .WithEndDate(currentDateTime)
                            .Build();


            task = await taskService.CreateTaskAsync(request);
        }

        public async Task DisposeAsync()
        {
            stepFakeRepository = null;
            taskService = null;
            task = null;
        }

        [Fact]
        public async Task Get_A_Step_By_Id()
        {
            var stepRequest = new CreateStepRequestBuilder()
                                .WithTitle(STEP_TITLE)
                                .WithDescription(STEP_DESCRIPTION)
                                .WithTaskId(task.Id)
                                .Build();

            var step = await taskService.AddStepToTaskAsync(stepRequest);

            var stepResponse = await taskService.GetStepByIdAsync(step.Id);

            stepResponse.Should().NotBeNull();
            stepResponse?.Id.Should().Be(step.Id);
        }

        [Fact]
        public async Task Get_All_Steps()
        {
            var stepRequest = new CreateStepRequestBuilder()
                                .WithTitle(STEP_TITLE)
                                .WithDescription(STEP_DESCRIPTION)
                                .WithTaskId(task.Id)
                                .Build();

            var amountOfCreatedSteps = 2;

            for(var i = 0; i < amountOfCreatedSteps; i++)
            {
                 await taskService.AddStepToTaskAsync(stepRequest);
            }

            var stepsResponse = await taskService.GetAllStepsForTaskAsync(task.Id);

            stepsResponse.Should().NotBeNull();
            stepsResponse.Count.Should().Be(amountOfCreatedSteps);
        }

        [Fact]
        public async Task Get_Only_Steps_From_The_Given_Task()
        {
            var stepRequest = new CreateStepRequestBuilder()
                                .WithTitle(STEP_TITLE)
                                .WithDescription(STEP_DESCRIPTION)
                                .WithTaskId(task.Id)
                                .Build();

            var amountOfCreatedSteps = 2;

            for (var i = 0; i < amountOfCreatedSteps; i++)
            {
                await taskService.AddStepToTaskAsync(stepRequest);
            }

            var task2 = new CreateTaskRequestBuilder()
                                    .WithTitle(TASK_TITLE)
                                    .WithEndDate(DateTime.UtcNow)
                                                    .Build();


            var task2Repsonse = await taskService.CreateTaskAsync(task2);

            var step3Request = new CreateStepRequestBuilder()
                    .WithTitle(STEP_TITLE)
                    .WithDescription(STEP_DESCRIPTION)
                    .WithTaskId(task2Repsonse.Id)
                    .Build();

            await taskService.AddStepToTaskAsync(step3Request);

            var stepsResponse = await taskService.GetAllStepsForTaskAsync(task.Id);

            stepsResponse.Should().NotBeNull();
            stepsResponse.Count.Should().Be(amountOfCreatedSteps);
        }
    }
}
