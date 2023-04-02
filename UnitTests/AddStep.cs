using FluentAssertions;
using TaskIt.Core.Entities;
using TaskIt.Core.Request.Builder;
using TaskIt.Infrastructure;
using TaskIt.Infrastructure.Fakes;

namespace UnitTests
{
    public class AddStep: IAsyncLifetime
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";
        private const string STEP_TITLE = "Step 1";
        private const string STEP_DESCRIPTION = "Cleaning the servace";

        private StepFakeRepository stepFakeRepository;
        private TaskService taskService;
        private TaskItem task;

        public AddStep()
        {

        }

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
        public void Create_A_Step_With_Data()
        {
            var stepResponse = new Step()
            {
                Title = STEP_TITLE,
                Description = STEP_DESCRIPTION
            };

            stepResponse.Title.Should().Be(STEP_TITLE);
            stepResponse.Description.Should().Be(STEP_DESCRIPTION);
        }

        [Fact]
        public async Task Add_A_Step_To_A_Task()
        {
            //ACT
            var stepRequest = new CreateStepRequestBuilder()
                .WithTitle(STEP_TITLE)
                .WithDescription(STEP_DESCRIPTION)
                .WithTaskId(task.Id)
                .Build();

            var stepResponse = await taskService.AddStepToTaskAsync(stepRequest);

            stepResponse.TaskId.Should().Be(task.Id);
            stepResponse.Title.Should().Be(STEP_TITLE);
            stepResponse.Description.Should().Be(STEP_DESCRIPTION);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task Can_Not_Add_Step_Without_Valid_Title(string? title)
        {
            var stepRequest = new CreateStepRequestBuilder()
                                    .WithTitle(title)
                                    .WithDescription(STEP_DESCRIPTION)
                                    .WithTaskId(task.Id)
                                    .Build();

            var listWithStepBeforeAct = await stepFakeRepository.GetAllAsync();

            await Assert.ThrowsAsync<InvalidStepItemException>(async () => await taskService.AddStepToTaskAsync(stepRequest));


            var listWithStepAfterAct = await stepFakeRepository.GetAllAsync();
            listWithStepAfterAct.Should().Equal(listWithStepBeforeAct);
        }

        [Fact]
        public async Task Can_Not_Add_Step_Without_Task()
        {
            var stepRequest = new CreateStepRequestBuilder()
                                    .WithTitle(STEP_TITLE)
                                    .WithDescription(STEP_DESCRIPTION)
                                    .Build();

            var listAmountWithStepBeforeAct = (await stepFakeRepository.GetAllAsync()).Count;

            //ACT
            await Assert.ThrowsAsync<InvalidStepItemException>(async () => await taskService.AddStepToTaskAsync(stepRequest));

            var listAmountWithStepAfterAct = (await stepFakeRepository.GetAllAsync()).Count;
            listAmountWithStepAfterAct.Should().Be(listAmountWithStepBeforeAct);
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

            var amountOfStepsBeforeAct = (await  stepFakeRepository.GetAllAsync()).Count;

            //ACT
            var response = await taskService.DeleteStepAsync(step.Id);

            //Assert            
            response.Should().BeTrue();

            var amountOfStepsAfterAct = (await stepFakeRepository.GetAllAsync()).Count;
            amountOfStepsAfterAct.Should().BeLessThan(amountOfStepsBeforeAct);
        }
    }
}
