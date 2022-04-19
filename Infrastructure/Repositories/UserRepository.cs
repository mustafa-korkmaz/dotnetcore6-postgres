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

        public Task<User?> GetByEmailAsync(string email)
        {
            return Entities.FirstOrDefaultAsync(o => o.Email == email);
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return Entities.FirstOrDefaultAsync(o => o.Username == username);
        }
    }
}
