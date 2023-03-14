using TaskIt.Core;
using TaskIt.Core.Entities;
using TaskIt.Core.Exceptions;
using TaskIt.Core.Request;

namespace UnitTests
{
    public class TaskService: ITaskService
    {

        public TaskService()
        {
        }

        public async Task<TaskItem> CreateTaskAsync(CreateTaskRequest createTaskRequest)
        {
            this.VerifyEndDate(createTaskRequest);

            var item = new TaskItem(createTaskRequest.Title, createTaskRequest.EndDate);

            return item;
        }

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