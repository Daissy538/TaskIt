﻿using FluentAssertions;
using TaskIt.Adapter.Fakes;
using TaskIt.Core.Request.Builder;

namespace UnitTests.Tasks
{
    public class DeleteTask
    {
        private const string TASK_TITLE = "Katten bak schoonmaken";
        private const string STEP_TITLE = "Step 1";
        private const string STEP_DESCRIPTION = "Cleaning the servace";
        
        private TaskService taskService;
        private TaskFakeRepository taskFakeRepository;
        private StepFakeRepository stepFakeRepository;

        public DeleteTask()
        {
            taskFakeRepository = new TaskFakeRepository();
            stepFakeRepository = new StepFakeRepository();
            taskService = new TaskService(taskFakeRepository, stepFakeRepository);
        }

        [Fact]
        public async Task Delete_Task()
        {
            var request = new CreateTaskRequestBuilder()
               .WithTitle(TASK_TITLE)
               .Build();

            var task = await taskService.CreateTaskAsync(request);
            var isDeleted = await taskService.DeleteTaskAsync(task.Id);

            var taskAfterAct = await taskFakeRepository.GetByIdAsync(task.Id);

            isDeleted.Should().BeTrue();
            taskAfterAct.Should().BeNull();
        }

        [Fact]
        public async Task Delete_Task_And_All_It_Steps()
        {
            var request = new CreateTaskRequestBuilder()
                               .WithTitle(TASK_TITLE)
                               .Build();

            var task = await taskService.CreateTaskAsync(request);

            var stepRequest = new CreateStepRequestBuilder()
                                .WithTitle(STEP_TITLE)
                                .WithDescription(STEP_DESCRIPTION)
                                .WithTaskId(task.Id)
                                .Build();

            var step = await taskService.AddStepToTaskAsync(stepRequest);


            var isDeleted = await taskService.DeleteTaskAsync(task.Id);
            var stepAfterAct = await stepFakeRepository.GetByIdAsync(step.Id);

            isDeleted.Should().BeTrue();
            stepAfterAct.Should().BeNull();
        }


        [Fact]
        public async Task Can_Not_Delete_Tasks_For_Unknow_Id()
        {
            var request = new CreateTaskRequestBuilder()
                               .WithTitle(TASK_TITLE)
                               .Build();

            var task = await taskService.CreateTaskAsync(request);

            var stepRequest = new CreateStepRequestBuilder()
                                .WithTitle(STEP_TITLE)
                                .WithDescription(STEP_DESCRIPTION)
                                .WithTaskId(task.Id)
                                .Build();

            var step = await taskService.AddStepToTaskAsync(stepRequest);


            var isDeleted = await taskService.DeleteTaskAsync(new Guid());
            var stepAfterAct = await stepFakeRepository.GetByIdAsync(step.Id);

            isDeleted.Should().BeFalse();
            stepAfterAct.Should().NotBeNull();
        }
    }
}
