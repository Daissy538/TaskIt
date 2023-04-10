using Microsoft.EntityFrameworkCore;
using TaskIt.Core.Entities;

namespace TaskIt.Adapter.SQL.Context
{
    public class TaskItSQLDbContext: DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }

        public DbSet<Step> Steps { get; set; }

        public TaskItSQLDbContext(DbContextOptions<TaskItSQLDbContext> options)
        : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
