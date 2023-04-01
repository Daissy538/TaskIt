using TaskIt.Core.Entities;
using TaskIt.Core.Exceptions;
using TaskIt.Core.Request;
using TaskIt.Core.Request.Builder;

namespace UnitTests
{
    public class AddStep
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";
        private const string StepTitle = "Step 1";
        private const string StepDescription = "Cleaning the servace";

        public AddStep()
        {

        }

        [Fact]
        public async Task Create_A_Step()
        {
            var step = new Step();

            Assert.IsType<Step>(step);
        }

        [Fact]
        public async Task Create_A_Step_With_Data()
        {
            var step = new Step(StepTitle, StepDescription);

            Assert.Equal(StepTitle, step.Title);
            Assert.Equal(StepDescription, step.Description);
        }

        //[Fact]
        //public async Task Add_A_Step_To_A_Task()
        //{
        //    var taskService = new TaskService();
        //    var currentDateTime = DateTime.UtcNow;

        //    var request = new CreateTaskRequestBuilder()
        //                    .WithTitle(TASK_TITLE)
        //                    .WithEndDate(currentDateTime)
        //                    .Build();

        //    var response = await taskService.CreateTaskAsync(request);

        //    await taskService.AddStepAsync(StepTitle, StepDescription);

        //    Assert.Equal(StepTitle, step.Title);
        //    Assert.Equal(StepDescription, step.Description);
        //}

        private void VerifyEndDate(CreateTaskRequest createTaskRequest)
        {
            if (createTaskRequest.EndDate.HasValue)
            {
                var endDateInThePast = createTaskRequest.EndDate.Value.Date < DateTime.UtcNow.Date;

                if (endDateInThePast)
                {
                    throw new InvalidTaskItemException("The end date is in the past");
                }
            }
        }
    }
}
