using Domain.Aggregates;
using Infrastructure.Persistance.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity> where TEntity : class, IEntity<TKey>
    {
        protected readonly DbSet<TEntity> Entities;
        private readonly PostgresDbContext _context;

        protected RepositoryBase(PostgresDbContext context)
        {
            _context = context;
            Entities = _context.Set<TEntity>();
        }

        public async Task<ListEntityResponse<TEntity>> ListAsync(ListEntityRequest request)
        {
            var result = new ListEntityResponse<TEntity>();

            var query = Entities.AsQueryable();

            if (request.IncludeRecordsTotal)
            {
                result.RecordsTotal = await query.CountAsync();
            }

            query = typeof(TKey) == typeof(Guid)
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderByDescending(p => p.Id);

            result.Items = await query
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToListAsync();

            return result;
        }

        public virtual async Task<TEntity?> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }
        public virtual async Task AddAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
        }
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Entities.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            var attachedEntity = Entities.Local.FirstOrDefault(e => e.Id!.Equals(entity.Id));

            if (attachedEntity != null)
            {
                _context.Entry(attachedEntity).State = EntityState.Detached;
            }

            Entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            //check entity state
            var dbEntityEntry = _context.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                Entities.Attach(entity);
                Entities.Remove(entity);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// sets and returns new entities which is different from main entity
        /// </summary>
        /// <typeparam name="TNewEntity"></typeparam>
        /// <typeparam name="TNewEntityKey"></typeparam>
        /// <returns></returns>
        protected DbSet<TNewEntity> GetEntities<TNewEntity, TNewEntityKey>() where TNewEntity : class, IEntity<TNewEntityKey>
        {
            return _context.Set<TNewEntity>();
        }
    }
}
