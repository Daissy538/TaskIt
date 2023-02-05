using TaskIt.Core.Entities;
using TaskIt.Core.Request;

namespace TaskIt.Core
{
    public interface ITaskService
    {
       TaskItem CreateTask(CreateTaskRequest createTaskRequest);
    }
}
