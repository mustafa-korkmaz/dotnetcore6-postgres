namespace Domain.Aggregates
{
    /// <summary>
    /// base entity abstraction with guid primary key
    /// </summary>
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public EntityBase()
        {
            Id = default(TKey);
            CreatedAt = DateTime.UtcNow;
        }

        public EntityBase(TKey id)
        {
            Id = id;
        }
    }

    public interface IEntity<TKey>
    {
        /// <summary>
        /// Primary key for table
        /// </summary>
        TKey Id { get; }

        DateTime CreatedAt { get; }
    }

    /// <summary>
    /// indicates that entity has a softDeletable option which prevents hard deletion from Db
    /// </summary>
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; }
    }

    public class ListEntityResponse<TEntity> where TEntity : class
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TEntity> Items { get; set; } = new List<TEntity>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long RecordsTotal { get; set; }
    }

    public class ListEntityRequest
    {
        public bool IncludeRecordsTotal { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}