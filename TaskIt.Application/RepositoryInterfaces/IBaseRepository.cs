namespace TaskIt.Core.RepositoryInterfaces
{
    public interface IBaseRepository<T>
    {
        public Task AddAsync(T item);

        public Task<T?> GetByIdAsync(Guid id);

        public Task<List<T>> GetAllAsync();

        public Task<bool> DeleteAsync(Guid id);
    }
}
