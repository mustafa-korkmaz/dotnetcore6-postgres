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
            optionsBuilder.UseNpgsql("Server=tai.db.elephantsql.com;Port=5432;Database=ocziorke;User Id=ocziorke;Password=t67v-NthY60rSJPxna--jB2iPE_WQPF8;CommandTimeout=20;");

            return new PostgresDbContext(optionsBuilder.Options);
        }
    }
}
