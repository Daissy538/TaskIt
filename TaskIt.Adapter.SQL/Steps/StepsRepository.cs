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

        public async Task<bool> DeleteAsync(Guid id)
        {
            var step = await GetByIdAsync(id);
            _dbContext.Steps.Remove(step);
            _dbContext.SaveChanges();

            return await GetByIdAsync(id) == default;
        }

        public async Task<List<Step>> GetAllAsync()
        {
            return await _dbContext.Steps.ToListAsync();
        }

        public async Task<Step?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Steps
                    .AsNoTracking()
                    .SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}
