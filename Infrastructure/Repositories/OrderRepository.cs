using Domain.Aggregates.Order;
using Infrastructure.Persistance.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class OrderRepository : RepositoryBase<Order, Guid>, IOrderRepository
    {
        public OrderRepository(PostgresDbContext context) : base(context)
        {
        }

        public override async Task<Order?> GetByIdAsync(object id)
        {
            var orderId = (Guid)id;

            return await Entities.Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
