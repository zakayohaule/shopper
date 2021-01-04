using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddLowStockAmountInStockModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sku_expirations");

            migrationBuilder.AddColumn<int>(
                name: "low_stock_amount",
                table: "skus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "low_stock_amount",
                table: "skus");

            migrationBuilder.CreateTable(
                name: "sku_expirations",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    expiration_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    sku_id = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    tenant_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
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
        }
    }
}
