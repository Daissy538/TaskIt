using TaskIt.Core.Entities;
using TaskIt.Core.Request;

namespace TaskIt.Core
{
    public interface ITaskService
    {
        Task<Step> AddStepToTaskAsync(CreateStepRequest createStepRequest);
        Task<TaskItem> CreateTaskAsync(CreateTaskRequest createTaskRequest);

        Task<bool> DeleteStepAsync(Guid id);
        Task<bool> DeleteTaskAsync(Guid Id);

        Task<TaskItem?> GetByIdAsync(Guid Id);

        Task<IList<TaskItem>> GetAllAsync();
    }
}
