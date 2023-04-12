using TaskIt.Application.Ports.RepositoryInterfaces;
using TaskIt.Core.Entities;

namespace TaskIt.Adapter.Fakes
{
    public class StepFakeRepository : IStepRepository
    {
        private readonly Dictionary<Guid, Step> Steps;

        public StepFakeRepository()
        {
            Steps = new Dictionary<Guid, Step>();
        }

        public async Task AddAsync(Step item)
        {
            var id = Guid.NewGuid();
            item.Id = id;

            Steps.Add(id, item);
        }

        public bool Delete(Guid id)
        {
            return Steps.Remove(id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return Steps.Remove(id);
        }

        public async Task<List<Step>> GetAllAsync()
        {
            return Steps.Values.ToList();
        }

        public async Task<List<Step>> GetAllForTaskAsync(Guid taksId)
        {
           return Steps.Values
                .Where(s => s.TaskId == taksId)
                .ToList();
        }

        public async Task<Step?> GetByIdAsync(Guid id)
        {
            return Steps.GetValueOrDefault(id);
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
