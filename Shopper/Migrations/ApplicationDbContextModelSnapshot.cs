﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shopper.Database;

namespace Shopper.Migrations
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

            modelBuilder.Entity("Shared.Mvc.Entities.Attribute", b =>
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

                    b.ToTable("attributes");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.AttributeOption", b =>
                {
                    b.Property<ushort>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("smallint unsigned");

                    b.Property<ushort>("AttributeId")
                        .HasColumnName("attribute_id")
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

                    b.HasIndex("AttributeId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("attribute_options");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.AppUser", b =>
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

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.Module", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("modules");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.Permission", b =>
                {
                    b.Property<ushort>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("DisplayName")
                        .HasColumnName("display_name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<short>("ModuleId")
                        .HasColumnName("module_id")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("DisplayName")
                        .IsUnique();

                    b.HasIndex("ModuleId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("permissions");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.Role", b =>
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

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.RoleClaim", b =>
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

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.UserClaim", b =>
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

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.UserLogin", b =>
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

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.UserRole", b =>
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

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.UserToken", b =>
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

            modelBuilder.Entity("Shared.Mvc.Entities.PriceType", b =>
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

                    b.ToTable("price_types");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Product", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImagePath")
                        .HasColumnName("image_path")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<ushort>("ProductCategoryId")
                        .HasColumnName("product_category_id")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("Slug")
                        .HasColumnName("slug")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.ProductAttributeOption", b =>
                {
                    b.Property<uint>("ProductId")
                        .HasColumnName("product_id")
                        .HasColumnType("int unsigned");

                    b.Property<ushort>("AttributeOptionId")
                        .HasColumnName("attribute_option_id")
                        .HasColumnType("smallint unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ProductId", "AttributeOptionId");

                    b.HasIndex("AttributeOptionId");

                    b.ToTable("product_attribute_options");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.ProductCategory", b =>
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

                    b.Property<ushort>("ProductGroupId")
                        .HasColumnName("product_group_id")
                        .HasColumnType("smallint unsigned");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProductGroupId");

                    b.ToTable("product_categories");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.ProductGroup", b =>
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

                    b.ToTable("product_groups");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Sku", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint unsigned");

                    b.Property<decimal>("BuyingPrice")
                        .HasColumnName("buying_price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsOnSale")
                        .HasColumnName("is_on_sale")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("MaximumDiscount")
                        .HasColumnName("maximum_discount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<uint>("ProductId")
                        .HasColumnName("product_id")
                        .HasColumnType("int unsigned");

                    b.Property<int>("Quantity")
                        .HasColumnName("quantity")
                        .HasColumnType("int");

                    b.Property<int>("RemainingQuantity")
                        .HasColumnName("remaining_quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("skus");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.SkuAttribute", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint unsigned");

                    b.Property<ushort>("AttributeOptionId")
                        .HasColumnName("attribute_option_id")
                        .HasColumnType("smallint unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("SkuId")
                        .HasColumnName("stock_keeping_unit_id")
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AttributeOptionId");

                    b.HasIndex("SkuId");

                    b.ToTable("sku_attributes");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.SkuSellingPrice", b =>
                {
                    b.Property<ushort>("PriceTypeId")
                        .HasColumnName("price_type_id")
                        .HasColumnType("smallint unsigned");

                    b.Property<ulong>("SkuId")
                        .HasColumnName("sku_id")
                        .HasColumnType("bigint unsigned");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PriceTypeId", "SkuId");

                    b.HasIndex("SkuId");

                    b.ToTable("sku_selling_prices");
                });

            modelBuilder.Entity("Shared.Mvc.Entities.AttributeOption", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.Attribute", "Attribute")
                        .WithMany("AttributeOptions")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.Permission", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.Identity.Module", "Module")
                        .WithMany("Permissions")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.RoleClaim", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.Identity.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.UserClaim", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.Identity.AppUser", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.UserLogin", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.Identity.AppUser", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.UserRole", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.Identity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Mvc.Entities.Identity.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Identity.UserToken", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.Identity.AppUser", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.Product", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.ProductAttributeOption", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.AttributeOption", "AttributeOption")
                        .WithMany()
                        .HasForeignKey("AttributeOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Mvc.Entities.Product", "Product")
                        .WithMany("AttributeOptions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.ProductCategory", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.ProductGroup", "ProductGroup")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.SkuAttribute", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.AttributeOption", "Option")
                        .WithMany()
                        .HasForeignKey("AttributeOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Mvc.Entities.Sku", "Sku")
                        .WithMany("SkuAttributes")
                        .HasForeignKey("SkuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shared.Mvc.Entities.SkuSellingPrice", b =>
                {
                    b.HasOne("Shared.Mvc.Entities.PriceType", "PriceType")
                        .WithMany()
                        .HasForeignKey("PriceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Mvc.Entities.Sku", "Sku")
                        .WithMany()
                        .HasForeignKey("SkuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
