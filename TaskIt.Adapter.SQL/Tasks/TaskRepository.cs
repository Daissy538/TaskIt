using Microsoft.EntityFrameworkCore;
using TaskIt.Core.Entities;
using TaskIt.Adapter.SQL.Context;
using TaskIt.Application.Ports.RepositoryInterfaces;

namespace TaskIt.Adapter.SQL.Tasks
{
    public class TaskRepository : ITaskRepository
    {
        private TaskItSQLDbContext _dbContext;

        public TaskRepository(TaskItSQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TaskItem item)
        {
            await _dbContext.AddAsync(item);
            _dbContext.SaveChanges();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await GetByIdAsync(id);

            if (task == default)
            {
                return false;
            }

             _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();

            return await GetByIdAsync(id) == default;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Tasks
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}
