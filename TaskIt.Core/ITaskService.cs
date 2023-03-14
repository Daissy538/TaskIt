using TaskIt.Core.Entities;
using TaskIt.Core.Request;

namespace TaskIt.Core
{
    public interface ITaskService
    {
       Task<TaskItem> CreateTaskAsync(CreateTaskRequest createTaskRequest);
    }
}
