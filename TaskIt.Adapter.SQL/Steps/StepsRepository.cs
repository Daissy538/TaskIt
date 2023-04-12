using Microsoft.EntityFrameworkCore;
using TaskIt.Adapter.SQL.Context;
using TaskIt.Application.Ports.RepositoryInterfaces;
using TaskIt.Core.Entities;

namespace TaskIt.Adapter.SQL.Steps
{
    public class StepsRepository : IStepRepository
    {
        private TaskItSQLDbContext _dbContext;

        public StepsRepository(TaskItSQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Step item)
        {
            await _dbContext.AddAsync(item);
            _dbContext.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var step = _dbContext.Steps
                    .AsNoTracking()
                    .SingleOrDefault(t => t.Id == id);

            if (step == default)
            {
                return false;
            }

            _dbContext.Steps.Remove(step);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var step = await GetByIdAsync(id);

            if (step == default)
            {
                return false;
            }

            _dbContext.Steps.Remove(step);
            _dbContext.SaveChanges();

            return await GetByIdAsync(id) == default;
        }

        public async Task<List<Step>> GetAllAsync()
        {
            return await _dbContext.Steps.ToListAsync();
        }

        public async Task<List<Step>> GetAllForTaskAsync(Guid taksId)
        {
            return await _dbContext.Steps
                .Where(s => s.TaskId == taksId)
                .ToListAsync();
        }

        public async Task<Step?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Steps
                    .AsNoTracking()
                    .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
