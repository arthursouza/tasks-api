namespace Domain.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddOrUpdateAsync(T entity);
        void Save();
        IQueryable<T> Queryable(bool tracking = true);
        void Remove(T entity);
    }
}