using Domain.Aggregates.Identity;
using Infrastructure.Persistance.Postgres;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(PostgresDbContext context) : base(context)
        {
        }

        public override Task<User?> GetByIdAsync(object id)
        {
            var userId = (Guid)id;

            return Entities.Include(p => p.Roles)
                .FirstOrDefaultAsync(p => p.Id == userId);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return Entities.Include(p => p.Roles)
                .FirstOrDefaultAsync(o => o.Email == email);
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return Entities.Include(p => p.Roles)
                .FirstOrDefaultAsync(o => o.Username == username);
        }
    }
}
