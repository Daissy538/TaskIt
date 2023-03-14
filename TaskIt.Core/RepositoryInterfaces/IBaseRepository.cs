namespace TaskIt.Core.RepositoryInterfaces
{
    public interface IBaseRepository<T>
    {
        public Task AddAsync(T item);

        public Task<T?> GetByIdAsync(Guid id);

        public Task<IList<T>> GetAllAsync();

        public Task<bool> DeleteAsync(Guid id);
    }
}
