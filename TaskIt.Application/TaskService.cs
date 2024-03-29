﻿using TaskIt.Application.Driven_Ports;
using TaskIt.Application.Ports.RepositoryInterfaces;
using TaskIt.Core;
using TaskIt.Core.Entities;
using TaskIt.Core.Request;

namespace TaskIt.Application
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IStepRepository _stepRepository;
        private readonly ISystemDateTimeClient _systemDateTimeClient;

        public TaskService(ITaskRepository taskRepository, IStepRepository stepRepository, ISystemDateTimeClient systemDateTimeClient)
        {
            _taskRepository = taskRepository;
            _stepRepository = stepRepository;
            _systemDateTimeClient = systemDateTimeClient;
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
            createTaskRequest.VerifyEndDate(_systemDateTimeClient.GetCurrentDateTimeUTC());

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

            var steps = await _stepRepository.GetAllForTaskAsync(Id);

            foreach (var step in steps)
            {
                _stepRepository.Delete(step.Id);
            }

            var isTaskDeleted = _taskRepository.Delete(Id);

            if (!isTaskDeleted)
            {
                return false;
            }

            await _taskRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IList<TaskItem>> GetAllAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<List<Step>> GetAllStepsForTaskAsync(Guid tasKId)
        {
            var steps = await _stepRepository.GetAllForTaskAsync(tasKId);
            return steps;
        }

        public async Task<TaskItem?> GetByIdAsync(Guid TaskId)
        {
            var task = await _taskRepository.GetByIdAsync(TaskId);

            if(task == null)
            {
                return null;
            }

            task.Steps = await GetAllStepsForTaskAsync(TaskId);
            return task;
        }

        public async Task<Step?> GetStepByIdAsync(Guid id)
        {
            return await _stepRepository.GetByIdAsync(id);
        }
    }
}