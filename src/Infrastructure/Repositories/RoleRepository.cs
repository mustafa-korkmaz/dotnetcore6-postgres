using Domain.Aggregates.Identity;
using Infrastructure.Persistance.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RoleRepository : RepositoryBase<Role, int>, IRoleRepository
    {
        private readonly PostgresDbContext _dbContext;
        public RoleRepository(PostgresDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IReadOnlyCollection<RoleClaim>> GetClaimsAsync(int[] roleIds)
        {
            return await _dbContext.RoleClaims
                .Include(p => p.Claim)
                .Where(p => roleIds.Contains(p.RoleId))
                .ToListAsync();
        }
    }
}
