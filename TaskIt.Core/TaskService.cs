using TaskIt.Core;
using TaskIt.Core.Exceptions;
using TaskIt.Core.Request;

namespace UnitTests
{
    public class TaskService: ITaskService
    {
        public TaskService()
        {
        }

        public TaskItem CreateTask(CreateTaskRequest createTaskRequest)
        {
            this.VerifyEndDate(createTaskRequest);

            return new TaskItem(createTaskRequest.Title, createTaskRequest.EndDate);
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