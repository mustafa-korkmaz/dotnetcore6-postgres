

namespace Domain.Aggregates
{
    /// <summary>
    /// base entity abstraction with guid primary key
    /// </summary>
    public abstract class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; private set; }

        public EntityBase()
        {
            Id = default(TKey);
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
    }

    /// <summary>
    /// indicates that entity has a softDeletable option which prevents hard deletion from Db
    /// </summary>
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; }
    }

    public class ListEntityResponse<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TEntity> Items { get; set; } = new List<TEntity>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long TotalCount { get; set; }
    }
}