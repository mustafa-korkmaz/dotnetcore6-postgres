using Infrastructure.Persistance.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Shipper.Api.Dal.Databases.Postgres
{
    /// <summary>
    /// for ef migrations and updates
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PostgresDbContext>
    {
        public PostgresDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=mute_1907;CommandTimeout=20;");

            return new PostgresDbContext(optionsBuilder.Options);
        }
    }
}
