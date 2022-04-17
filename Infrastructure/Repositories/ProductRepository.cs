using Domain.Aggregates.Product;
using Infrastructure.Persistance.Postgres;

namespace Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product, long>, IProductRepository
    {
        public ProductRepository(PostgresDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyCollection<Product>> ListByIdsAsync(string[] ids)
        {
            return null;
           // return await Collection.Find(p => ids.Contains(p.Id)).ToListAsync();
        }
    }
}
