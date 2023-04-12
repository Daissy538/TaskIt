using TaskIt.Core.Entities;

namespace TaskIt.Application.Ports.RepositoryInterfaces
{
    public interface IStepRepository : IBaseRepository<Step>
    {
        Task<List<Step>> GetAllForTaskAsync(Guid taksId);
    }
}
