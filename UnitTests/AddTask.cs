using TaskIt.Core.Exceptions;
using TaskIt.Core.Request.Builder;

namespace UnitTests
{
    public class AddTask
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";

        public AddTask()
        {
                
        }

        [Fact]
        public void Add_A_Task()
        {
            var taskService = new TaskService();

            var request = new CreateTaskRequestBuilder()
                .WithTitle(TASK_TITLE)
                .Build();

            var response = taskService.CreateTask(request);

            Assert.Equal(TASK_TITLE, response.Title);
        }
        
        [Fact]
        public void Add_Task_With_End_Date()
        {
            var taskService = new TaskService();
            var currentDateTime = DateTime.UtcNow;

            var request = new CreateTaskRequestBuilder()
                            .WithTitle(TASK_TITLE)
                            .WithEndDate(currentDateTime)
                            .Build();

            var response = taskService.CreateTask(request);

            Assert.Equal(TASK_TITLE, response.Title);
            Assert.Equal(currentDateTime, response.EndDate);
        }

        [Fact]
        public void Do_Not_Add_Task_With_End_Date_In_The_Past()
        {
            var taskService = new TaskService();
            var currentDateTime = DateTime.UtcNow.AddDays(-1);

            var request = new CreateTaskRequestBuilder()
                .WithTitle(TASK_TITLE)
                .WithEndDate(currentDateTime)
                .Build();

            Assert.Throws<InvalidTaskItemException>(() => taskService.CreateTask(request));
        }
    }
}
