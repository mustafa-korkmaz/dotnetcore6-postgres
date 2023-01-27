namespace Domain.Aggregates
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<ListEntityResponse<TEntity>> ListAsync(ListEntityRequest request);
        Task<TEntity?> GetByIdAsync(object id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}