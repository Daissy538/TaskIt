using TaskIt.Core.Request.Builder;
using TaskIt.Core;
using TaskIt.Adapter.Fakes;
using TaskIt.Core.Entities;
using FluentAssertions;

namespace UnitTests.Step
{
    public class DeleteStep : IAsyncLifetime
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
        public async Task Delete_A_Step_From_A_Task()
        {
            var stepRequest = new CreateStepRequestBuilder()
                .WithTitle(STEP_TITLE)
                .WithDescription(STEP_DESCRIPTION)
                .WithTaskId(task.Id)
                .Build();

            var step = await taskService.AddStepToTaskAsync(stepRequest);

            var amountOfStepsBeforeAct = (await stepFakeRepository.GetAllAsync()).Count;

            //ACT
            var response = await taskService.DeleteStepAsync(step.Id);

            //Assert            
            response.Should().BeTrue();

            var amountOfStepsAfterAct = (await stepFakeRepository.GetAllAsync()).Count;
            amountOfStepsAfterAct.Should().BeLessThan(amountOfStepsBeforeAct);
        }
    }
}
