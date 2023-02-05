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

        public void Add(TaskItem item)
        {
            var id = Guid.NewGuid();
            item.Id = id;
            Tasks.Add(id, item);
        }

        public IList<TaskItem> GetAll()
        {
            return Tasks.Values.ToList();
        }

        public TaskItem GetById(Guid id)
        {
            var repsonse = Tasks.SingleOrDefault(t => t.Key == id);
            return repsonse.Value;
        }
    }
}