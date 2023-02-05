namespace TaskIt.Core.RepositoryInterfaces
{
    public interface IBaseRepository<T>
    {
        public void Add(T item);

        public T? GetById(Guid id);

        public IList<T> GetAll();
    }
}
