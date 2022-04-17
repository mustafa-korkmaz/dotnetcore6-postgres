﻿// <auto-generated />
using System;
using Infrastructure.Persistance.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(PostgresDbContext))]
    partial class PostgresDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Aggregates.Order.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Aggregates.Order.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Domain.Aggregates.Product.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("integer");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7f9fcc26-c38c-46bd-86a7-b7b3d5959b78"),
                            ConcurrencyStamp = "f0e32c74-6510-484d-a6f4-db6a79eb82e4",
                            Name = "car_owner",
                            NormalizedName = "CAR_OWNER"
                        },
                        new
                        {
                            Id = new Guid("e964fe31-ba9a-4ee6-98c1-7fa84767868d"),
                            ConcurrencyStamp = "a620a23a-3760-4d4f-a095-79ffe5fd9a39",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("0967d456-60a8-43de-9ac8-5f15dfaa1909"),
                            ConcurrencyStamp = "7473ba90-3e20-491d-a4c2-ecbc7f22ec5f",
                            Name = "transporter",
                            NormalizedName = "TRANSPORTER"
                        });
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ClaimValue")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "test_car_owner_role_claim",
                            ClaimValue = "test_car_owner_role_claim",
                            RoleId = new Guid("7f9fcc26-c38c-46bd-86a7-b7b3d5959b78")
                        });
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("varchar(36)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NameSurname")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar(8000)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(14)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("87622649-96c8-40b5-bcef-8351b0883b49"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "024e1046-752c-4943-9373-5ac78ab5601a",
                            CreatedAt = new DateTimeOffset(new DateTime(2021, 7, 29, 21, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "mustafakorkmazdev@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NameSurname = "Mustafa Korkmaz",
                            NormalizedEmail = "MUSTAFAKORKMAZDEV@GMAIL.COM",
                            NormalizedUserName = "MUSTAFAKORKMAZDEV@GMAIL.COM",
                            PasswordHash = "AD5bszN5VbOZSQW+1qcXQb08ElGNt9uNoTrsNenNHSsD1g2Gp6ya4+uFJWmoUsmfng==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "951a4c00-20d0-4d65-9d4a-7db4001c834c",
                            TwoFactorEnabled = false,
                            UserName = "mustafakorkmazdev@gmail.com"
                        });
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ClaimValue")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "test_user_claim",
                            ClaimValue = "test_user_claim",
                            UserId = new Guid("87622649-96c8-40b5-bcef-8351b0883b49")
                        });
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Aggregates.Order.OrderItem", b =>
                {
                    b.HasOne("Domain.Aggregates.Order.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUserRole<System.Guid>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUserToken<System.Guid>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Aggregates.Order.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
