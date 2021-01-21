﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopperAdmin.Database;

namespace ShopperAdmin.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Database", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("smallint");

                    b.Property<string>("ConnectionString")
                        .HasColumnName("connection_string")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ConnectionString")
                        .IsUnique();

                    b.ToTable("databases");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.AppUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnName("access_failed_count")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("concurrency_stamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnName("email_confirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FullName")
                        .HasColumnName("full_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("HasResetPassword")
                        .HasColumnName("has_reset_password")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnName("lockout_enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnName("lockout_end")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnName("normalized_email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnName("normalized_user_name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnName("password_hash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("phone_number")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnName("phone_number_confirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnName("security_stamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnName("two_factor_enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasColumnName("user_name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.Module", b =>
                {
                    b.Property<ushort>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("smallint unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("modules");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.Permission", b =>
                {
                    b.Property<ushort>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("smallint unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DisplayName")
                        .HasColumnName("display_name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<ushort>("ModuleId")
                        .HasColumnName("module_id")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("DisplayName")
                        .IsUnique();

                    b.HasIndex("ModuleId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("permissions");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("concurrency_stamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnName("display_name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnName("normalized_name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnName("claim_type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .IsRequired()
                        .HasColumnName("claim_value")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<long>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasAlternateKey("RoleId", "ClaimValue");

                    b.ToTable("role_claims");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("user_claims");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<long>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("user_logins");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("user_role");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.UserToken", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasColumnName("login_provider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Value")
                        .HasColumnName("value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("user_tokens");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Address")
                        .HasColumnName("address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Code")
                        .HasColumnName("code")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConnectionString")
                        .HasColumnName("connection_string")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Domain")
                        .HasColumnName("domain")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LogoPath")
                        .HasColumnName("logo_path")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber1")
                        .HasColumnName("phone_number_1")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber2")
                        .HasColumnName("phone_number_2")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("SubscriptionType")
                        .HasColumnName("subscription_type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnName("valid_to")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Domain")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("tenants");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.TenantSubscription", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsCurrent")
                        .HasColumnName("is_current")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("SubscriptionDate")
                        .HasColumnName("subscription_date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("SubscriptionEndDate")
                        .HasColumnName("subscription_end_date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SubscriptionType")
                        .HasColumnName("subscription_type")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnName("tenant_id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("tenant_subscriptions");
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.Permission", b =>
                {
                    b.HasOne("ShopperAdmin.Mvc.Entities.Identity.Module", "Module")
                        .WithMany("Permissions")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.RoleClaim", b =>
                {
                    b.HasOne("ShopperAdmin.Mvc.Entities.Identity.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.UserClaim", b =>
                {
                    b.HasOne("ShopperAdmin.Mvc.Entities.Identity.AppUser", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.UserLogin", b =>
                {
                    b.HasOne("ShopperAdmin.Mvc.Entities.Identity.AppUser", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.UserRole", b =>
                {
                    b.HasOne("ShopperAdmin.Mvc.Entities.Identity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopperAdmin.Mvc.Entities.Identity.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.Identity.UserToken", b =>
                {
                    b.HasOne("ShopperAdmin.Mvc.Entities.Identity.AppUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShopperAdmin.Mvc.Entities.TenantSubscription", b =>
                {
                    b.HasOne("ShopperAdmin.Mvc.Entities.Tenant", "Tenant")
                        .WithMany("TenantSubscriptions")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
