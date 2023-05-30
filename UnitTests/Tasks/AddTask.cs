using FluentAssertions;
using TaskIt.Core.Exceptions;
using TaskIt.Core.Request.Builder;
using TaskIt.Adapter.Fakes;
using TaskIt.Application;
using TaskIt.Adapter.Fake.Fakes;

namespace UnitTests.Tasks
{
    public class AddTask
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";
        private const string CURRENT_DATETIME = "2011-03-21 13:26";

        private TaskService taskService;
        private TaskFakeRepository taskFakeRepository;
        private SystemDateTimeClient systemDateTimeClient;

        public AddTask()
        {
            taskFakeRepository = new TaskFakeRepository();
            systemDateTimeClient = new SystemDateTimeClient(CURRENT_DATETIME);
            taskService = new TaskService(taskFakeRepository, new StepFakeRepository(), systemDateTimeClient);
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
            var currentDateTime = systemDateTimeClient.GetCurrentDateTimeUTC();

            var request = new CreateTaskRequestBuilder()
                            .WithTitle(TASK_TITLE)
                            .WithEndDate(currentDateTime)
                            .Build();

            var response = await taskService.CreateTaskAsync(request);

            response.Title.Should().Be(TASK_TITLE);
            response.EndDate.Should().Be(currentDateTime);
        }

        [Fact]
        public async Task Do_Not_Add_Task_With_End_Date_In_The_Past()
        {
            var currentDateTime = systemDateTimeClient
                                    .GetCurrentDateTimeUTC()
                                    .AddDays(-1);

            var request = new CreateTaskRequestBuilder()
                .WithTitle(TASK_TITLE)
                .WithEndDate(currentDateTime)
                .Build();

            var act = taskService.CreateTaskAsync(request);
            await Assert.ThrowsAsync<InvalidTaskItemException>(async () => await act);
        }
    }
}
