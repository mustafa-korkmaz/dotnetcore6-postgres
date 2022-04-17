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
            optionsBuilder.UseNpgsql("Server=207.154.194.168;Port=5410;Database=postgres;User Id=postgres;Password=Shipper2021!;CommandTimeout=20;");

            return new PostgresDbContext(optionsBuilder.Options);
        }
    }
}
