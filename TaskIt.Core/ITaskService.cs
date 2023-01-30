using TaskIt.Core.Request;
using UnitTests;

namespace TaskIt.Core
{
    public interface ITaskService
    {
        TaskItem CreateTask(CreateTaskRequest createTaskRequest);
    }
}
