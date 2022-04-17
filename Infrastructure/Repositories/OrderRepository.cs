using Domain.Aggregates.Order;
using Infrastructure.Persistance.Postgres;

namespace Infrastructure.Repositories
{
    internal class OrderRepository : RepositoryBase<Order, Guid>, IOrderRepository
    {
        public OrderRepository(PostgresDbContext context) : base(context)
        {

        }
    }
}
