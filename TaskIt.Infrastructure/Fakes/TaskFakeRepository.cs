using TaskIt.Core.Entities;
using TaskIt.Core.RepositoryInterfaces;

namespace TaskIt.Infrastructure
{

    public class TaskFakeRepository : ITaskRepository
    {
        private readonly Dictionary<Guid, TaskItem> Tasks;

        public TaskFakeRepository()
        {
            Tasks= new Dictionary<Guid, TaskItem>();
        }

        public async Task AddAsync(TaskItem item)
        {
            var id = Guid.NewGuid();
            item.Id = id;
            Tasks.Add(id, item);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return Tasks.Remove(id);
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return Tasks.Values.ToList();
        }

        public async Task<TaskItem> GetByIdAsync(Guid id)
        {
            var repsonse = Tasks.SingleOrDefault(t => t.Key == id);
            return repsonse.Value;
        }
    }
}