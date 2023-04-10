using TaskIt.Core.Request.Builder;
using TaskIt.Adapter.Fakes;
using TaskIt.Adapter;
using TaskIt.Core.Entities;
using FluentAssertions;
using TaskIt.Core;

namespace UnitTests.Step
{
    public class RetrieveStep : IAsyncLifetime
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";
        private const string STEP_TITLE = "Step 1";
        private const string STEP_DESCRIPTION = "Cleaning the servace";

        private StepFakeRepository stepFakeRepository;
        private ITaskService taskService;
        private TaskItem task;

        public async Task InitializeAsync()
        {
            stepFakeRepository = new StepFakeRepository();

            taskService = new TaskService(new TaskFakeRepository(), stepFakeRepository);
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
    }
}
