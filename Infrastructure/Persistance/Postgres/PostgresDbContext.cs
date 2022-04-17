using System.ComponentModel.DataAnnotations;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Postgres
{
    public class PostgresDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim<Guid>,
              ApplicationUserRole<Guid>, ApplicationUserLogin<Guid>, ApplicationRoleClaim<Guid>, ApplicationUserToken<Guid>>
    {
        public PostgresDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            #region identity user modifications

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.ConcurrencyStamp)
                .HasColumnType("varchar(36)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.Email)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.NormalizedEmail)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
               .Property(p => p.UserName)
               .HasColumnType("varchar(50)")
               .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.NormalizedUserName)
                .HasColumnType("varchar(50)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.PasswordHash)
                .HasColumnType("varchar(8000)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.PhoneNumber)
                .HasColumnType("varchar(14)");

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.SecurityStamp)
                .HasColumnType("varchar(100)")
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(p => p.NameSurname)
                .HasColumnType("varchar(100)");

            #endregion identity user modifications

         

            #region role modifications

            modelBuilder.Entity<ApplicationRole>()
                .Property(p => p.ConcurrencyStamp)
                .HasColumnType("varchar(36)")
                .IsRequired();

            modelBuilder.Entity<ApplicationRole>()
                .Property(p => p.Name)
                .HasColumnType("varchar(20)")
                .IsRequired();

            modelBuilder.Entity<ApplicationRole>()
                .Property(p => p.NormalizedName)
                .HasColumnType("varchar(20)")
                .IsRequired();

            #endregion role modifications


            #region user login modifications

            modelBuilder.Entity<ApplicationUserLogin<Guid>>()
                .Property(p => p.LoginProvider)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationUserLogin<Guid>>()
                .Property(p => p.ProviderKey)
                .HasColumnType("varchar(128)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationUserLogin<Guid>>()
                .Property(p => p.ProviderDisplayName)
                .HasColumnType("varchar(100)");


            #endregion user login modifications

            #region user token modifications

            modelBuilder.Entity<ApplicationUserToken<Guid>>()
                .Property(p => p.LoginProvider)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationUserToken<Guid>>()
                .Property(p => p.Name)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationUserToken<Guid>>()
                .Property(p => p.Value)
                .HasColumnType("varchar(500)")
                  .IsRequired();

            #endregion user token modifications

            #region user claim modifications

            modelBuilder.Entity<ApplicationUserClaim<Guid>>()
                .Property(p => p.ClaimType)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationUserClaim<Guid>>()
                .Property(p => p.ClaimValue)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            #endregion user claim modifications

            #region role claim modifications

            modelBuilder.Entity<ApplicationRoleClaim<Guid>>()
                .Property(p => p.ClaimType)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            modelBuilder.Entity<ApplicationRoleClaim<Guid>>()
                .Property(p => p.ClaimValue)
                .HasColumnType("varchar(50)")
                  .IsRequired();

            #endregion role claim modifications

            #region seed

            modelBuilder.Entity<ApplicationRole>().HasData(
                CreateRole(Guid.Parse("7f9fcc26-c38c-46bd-86a7-b7b3d5959b78"),
                    "car_owner", "f0e32c74-6510-484d-a6f4-db6a79eb82e4"),
                CreateRole(Guid.Parse("e964fe31-ba9a-4ee6-98c1-7fa84767868d"),
                    "admin", "a620a23a-3760-4d4f-a095-79ffe5fd9a39"),
                CreateRole(Guid.Parse("0967d456-60a8-43de-9ac8-5f15dfaa1909"),
                    "transporter", "7473ba90-3e20-491d-a4c2-ecbc7f22ec5f")
            );

            ApplicationRole CreateRole(Guid id, string name, string concurrencyStamp)
            {
                return new()
                {
                    Id = id,
                    Name = name,
                    NormalizedName = name.ToUpper(),
                    ConcurrencyStamp = concurrencyStamp
                };
            }

            modelBuilder.Entity<ApplicationRoleClaim<Guid>>().HasData(
                CreateRoleClaim(1, Guid.Parse("7f9fcc26-c38c-46bd-86a7-b7b3d5959b78"),
                    "test_car_owner_role_claim", "test_car_owner_role_claim")
            );

            ApplicationRoleClaim<Guid> CreateRoleClaim(int id, Guid roleId, string type, string value)
            {
                return new()
                {
                    Id = id,
                    RoleId = roleId,
                    ClaimType = type,
                    ClaimValue = value
                };
            }



            modelBuilder.Entity<ApplicationUser>().HasData(

           CreateUser(Guid.Parse("87622649-96c8-40b5-bcef-8351b0883b49"),
               "Mustafa Korkmaz",
               "mustafakorkmazdev@gmail.com"));


            ApplicationUser CreateUser(Guid id, string name, string email)
            {
                return new ApplicationUser
                {
                    Id = id,
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    CreatedAt = new DateTime(2021, 7, 30),
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    SecurityStamp = "951a4c00-20d0-4d65-9d4a-7db4001c834c",
                    ConcurrencyStamp = "024e1046-752c-4943-9373-5ac78ab5601a",
                    PasswordHash = "AD5bszN5VbOZSQW+1qcXQb08ElGNt9uNoTrsNenNHSsD1g2Gp6ya4+uFJWmoUsmfng==",
                    NameSurname = name
                };
            }

            modelBuilder.Entity<ApplicationUserClaim<Guid>>().HasData(
                CreateUserClaim(1, Guid.Parse("87622649-96c8-40b5-bcef-8351b0883b49"),
                    "test_user_claim", "test_user_claim")
            );

            ApplicationUserClaim<Guid> CreateUserClaim(int id, Guid userId, string type, string value)
            {
                return new()
                {
                    Id = id,
                    UserId = userId,
                    ClaimType = type,
                    ClaimValue = value
                };
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