using FluentAssertions;
using TaskIt.Core.Exceptions;
using TaskIt.Core.Request.Builder;
using TaskIt.Infrastructure;
using TaskIt.Infrastructure.Fakes;

namespace UnitTests
{
    public class AddTask
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";
        private TaskService taskService;
        private TaskFakeRepository taskFakeRepository;

        public AddTask()
        {
            taskFakeRepository = new TaskFakeRepository();
            taskService = new TaskService(taskFakeRepository, new StepFakeRepository());
        }

        [Fact]
        public async Task Add_A_Task()
        {
            var request = new CreateTaskRequestBuilder()
                .WithTitle(TASK_TITLE)
                .Build();

            var response = await taskService.CreateTaskAsync(request);

            response.Title.Should().Be(TASK_TITLE);
        }
        
        [Fact]
        public async Task Add_Task_With_End_DateAsync()
        {
            var currentDateTime = DateTime.UtcNow;

            var request = new CreateTaskRequestBuilder()
                            .WithTitle(TASK_TITLE)
                            .WithEndDate(currentDateTime)
                            .Build();

            var response =  await taskService.CreateTaskAsync(request);

            response.Title.Should().Be(TASK_TITLE);
            response.EndDate.Should().Be(currentDateTime);
        }

        [Fact]
        public async Task Do_Not_Add_Task_With_End_Date_In_The_Past()
        {
            var currentDateTime = DateTime.UtcNow.AddDays(-1);

            var request = new CreateTaskRequestBuilder()
                .WithTitle(TASK_TITLE)
                .WithEndDate(currentDateTime)
                .Build();

            var act = taskService.CreateTaskAsync(request);
            await Assert.ThrowsAsync<InvalidTaskItemException>(async () => await act);
        }
    }
}
