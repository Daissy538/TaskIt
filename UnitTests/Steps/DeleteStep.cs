using TaskIt.Core.Request.Builder;
using TaskIt.Core;
using TaskIt.Adapter.Fakes;
using TaskIt.Core.Entities;
using FluentAssertions;
using TaskIt.Application;
using TaskIt.Adapter.Fake.Fakes;

namespace UnitTests.Step
{
    public class DeleteStep : IAsyncLifetime
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

        [Fact]
        public async Task Can_Not_Delete_A_Not_Existing_Step()
        {
            var stepRequest = new CreateStepRequestBuilder()
                                    .WithTitle(STEP_TITLE)
                                    .WithDescription(STEP_DESCRIPTION)
                                    .WithTaskId(task.Id)
                                    .Build();

            var step = await taskService.AddStepToTaskAsync(stepRequest);

            var amountOfStepsBeforeAct = (await stepFakeRepository.GetAllAsync()).Count;

            //ACT
            var response = await taskService.DeleteStepAsync(new Guid());

            //Assert            
            response.Should().BeFalse();
        }
    }
}
