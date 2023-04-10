using TaskIt.Application.Ports.RepositoryInterfaces;
using TaskIt.Core;
using TaskIt.Core.Entities;
using TaskIt.Core.Exceptions;
using TaskIt.Core.Request;

namespace UnitTests
{
    public class TaskService: ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IStepRepository _stepRepository;

        public TaskService(ITaskRepository taskRepository, IStepRepository stepRepository)
        {
            _taskRepository = taskRepository;
            _stepRepository = stepRepository;
        }

        public async Task<Step> AddStepToTaskAsync(CreateStepRequest createStepRequest)
        {
           createStepRequest.VerifyData();
           var step = createStepRequest.GenerateStep();

            await _stepRepository.AddAsync(step);

            return step;
        }

        public async Task<TaskItem> CreateTaskAsync(CreateTaskRequest createTaskRequest)
        {
            createTaskRequest.VerifyEndDate();

            TaskItem item = new TaskItem(createTaskRequest.Title, createTaskRequest.EndDate);

            await _taskRepository.AddAsync(item);

            return item;
        }

        public async Task<bool> DeleteStepAsync(Guid id)
        {
           return await _stepRepository.DeleteAsync(id);
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

        public async Task<Step?> GetStepByIdAsync(Guid id)
        {
            return await _stepRepository.GetByIdAsync(id);
        }
    }
}