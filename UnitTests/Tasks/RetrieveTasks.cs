using FluentAssertions;
using TaskIt.Adapter.Fakes;
using TaskIt.Core.Entities;
using TaskIt.Core.Request.Builder;

namespace UnitTests.Tasks
{
    public class RetrieveTasks
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";
        private const string STEP_TITLE = "Step 1";
        private const string STEP_DESCRIPTION = "Cleaning the servace";

        private TaskService taskService;
        private TaskFakeRepository taskFakeRepository;
        private StepFakeRepository stepFakeRepository;

        public RetrieveTasks()
        {
            taskFakeRepository = new TaskFakeRepository();
            stepFakeRepository = new StepFakeRepository();
            taskService = new TaskService(taskFakeRepository, stepFakeRepository);
        }

        [Fact]
        public async void Retrieve_Task_Details()
        {
            TaskItem task;
            taskService = new TaskService(new TaskFakeRepository(), stepFakeRepository);
                var currentDateTime = DateTime.UtcNow;

            var request = new CreateTaskRequestBuilder()
            .WithTitle(TASK_TITLE)
            .WithEndDate(currentDateTime)
            .Build();

            task = await taskService.CreateTaskAsync(request);
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

            var taskResponse = await taskService.GetByIdAsync(task.Id);

            task.Title.Should().Be(TASK_TITLE);
            task.EndDate.Should().Be(currentDateTime);
            task.Steps.Should().HaveCount(amountOfCreatedSteps);
        }
    }
}
