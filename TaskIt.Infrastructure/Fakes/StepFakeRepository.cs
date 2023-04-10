using TaskIt.Application.RepositoryInterfaces;
using TaskIt.Core.Entities;

namespace TaskIt.Infrastructure.Fakes
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            return Steps.Remove(id);
        }

        public async Task<List<Step>> GetAllAsync()
        {
            return Steps.Values.ToList();
        }

        public async Task<Step?> GetByIdAsync(Guid id)
        {
            return Steps.GetValueOrDefault(id);
        }
    }
}
