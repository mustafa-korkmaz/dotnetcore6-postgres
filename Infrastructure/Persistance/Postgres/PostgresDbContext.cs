using System.ComponentModel.DataAnnotations;
using Domain.Aggregates.Identity;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Postgres
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Claim> Claims { get; set; }

        public DbSet<RoleClaim> RoleClaims { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region product modifications

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            modelBuilder.Entity<Product>()
              .Property(p => p.Sku)
              .HasColumnType("varchar(50)")
              .IsRequired();

            #endregion product modifications

            #region order modifications

            modelBuilder.Entity<Order>()
                .Property(p => p.UserId)
                .IsRequired();

            #endregion order modifications

            #region order item modifications

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.ProductId)
                .IsRequired();

            #endregion order item modifications

            #region identity user modifications

            modelBuilder.Entity<User>()
                .Property(p => p.Email)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<User>()
               .Property(p => p.Username)
               .HasColumnType("varchar(50)")
               .IsRequired();

            modelBuilder.Entity<User>()
                .Property(p => p.PasswordHash)
                .HasColumnType("varchar(8000)")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(p => p.NameSurname)
                .HasColumnType("varchar(100)");

            modelBuilder.Entity<User>()
                 .Property(p => p.CreatedAt)
                 .IsRequired();

            #endregion identity user modifications

            #region role modifications

            modelBuilder.Entity<Role>()
                .Property(p => p.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();

            #endregion role modifications

            #region user role modifications

            modelBuilder.Entity<UserRole>()
                .Property(p => p.UserId)
                  .IsRequired();

            modelBuilder.Entity<UserRole>()
               .Property(p => p.RoleId)
                 .IsRequired();

            #endregion user role modifications

            #region claim modifications

            modelBuilder.Entity<Claim>()
                .Property(p => p.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();

            #endregion claim modifications

            #region role claim modifications

            modelBuilder.Entity<RoleClaim>()
                .Property(p => p.ClaimId)
                  .IsRequired();

            modelBuilder.Entity<RoleClaim>()
               .Property(p => p.RoleId)
                 .IsRequired();

            #endregion role claim modifications

            #region seed

            modelBuilder.Entity<Role>().HasData(
                CreateRole(1, "freemium"),
                CreateRole(2, "paid"),
                CreateRole(3, "admin")
            );

            Role CreateRole(int id, string name)
            {
                return new Role(id, name);
            }

            modelBuilder.Entity<User>().HasData(

           CreateUser(Guid.Parse("87622649-96c8-40b5-bcef-8351b0883b49"),
               "Mustafa Korkmaz",
               "mustafakorkmazdev@gmail.com"));


            User CreateUser(Guid id, string name, string email)
            {
                return new User(id, email, email, name, true, "AD5bszN5VbOZSQW+1qcXQb08ElGNt9uNoTrsNenNHSsD1g2Gp6ya4+uFJWmoUsmfng==");
            }

            #endregion seed
        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                                 || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                                 || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}