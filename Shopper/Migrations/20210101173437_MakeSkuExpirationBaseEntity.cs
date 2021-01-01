using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class MakeSkuExpirationBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_categories_product_categories_parent_category_id",
                table: "product_categories");

            migrationBuilder.AlterColumn<ushort>(
                name: "parent_category_id",
                table: "product_categories",
                nullable: true,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");

            migrationBuilder.CreateTable(
                name: "sku_expirations",
                columns: table => new
                {
                    id = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tenant_id = table.Column<Guid>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    sku_id = table.Column<ulong>(nullable: false),
                    expiration_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sku_expirations", x => x.id);
                    table.ForeignKey(
                        name: "FK_sku_expirations_skus_sku_id",
                        column: x => x.sku_id,
                        principalTable: "skus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sku_expirations_tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sku_expirations_sku_id",
                table: "sku_expirations",
                column: "sku_id");

            migrationBuilder.CreateIndex(
                name: "IX_sku_expirations_tenant_id",
                table: "sku_expirations",
                column: "tenant_id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_categories_product_categories_parent_category_id",
                table: "product_categories",
                column: "parent_category_id",
                principalTable: "product_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_categories_product_categories_parent_category_id",
                table: "product_categories");

            migrationBuilder.DropTable(
                name: "sku_expirations");

            migrationBuilder.AlterColumn<ushort>(
                name: "parent_category_id",
                table: "product_categories",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(ushort),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_product_categories_product_categories_parent_category_id",
                table: "product_categories",
                column: "parent_category_id",
                principalTable: "product_categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
