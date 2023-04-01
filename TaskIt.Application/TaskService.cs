using TaskIt.Core;
using TaskIt.Core.Entities;
using TaskIt.Core.Exceptions;
using TaskIt.Core.RepositoryInterfaces;
using TaskIt.Core.Request;

namespace UnitTests
{
    public class TaskService: ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItem> CreateTaskAsync(CreateTaskRequest createTaskRequest)
        {
            this.VerifyEndDate(createTaskRequest);

            var item = new TaskItem(createTaskRequest.Title, createTaskRequest.EndDate);

            await _taskRepository.AddAsync(item);

            return item;
        }

        public async Task<bool> DeleteTaskAsync(Guid Id)
        {
            return await _taskRepository.DeleteAsync(Id);
        }

        public async Task<IList<TaskItem>> GetAllAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid Id)
        {
           return await _taskRepository.GetByIdAsync(Id);
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