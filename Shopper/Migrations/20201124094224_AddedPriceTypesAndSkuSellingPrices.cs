using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddedPriceTypesAndSkuSellingPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "price_types",
                columns: table => new
                {
                    id = table.Column<ushort>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_price_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sku_selling_prices",
                columns: table => new
                {
                    sku_id = table.Column<ulong>(nullable: false),
                    price_type_id = table.Column<ushort>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sku_selling_prices", x => new { x.price_type_id, x.sku_id });
                    table.ForeignKey(
                        name: "FK_sku_selling_prices_price_types_price_type_id",
                        column: x => x.price_type_id,
                        principalTable: "price_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sku_selling_prices_skus_sku_id",
                        column: x => x.sku_id,
                        principalTable: "skus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_price_types_name",
                table: "price_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sku_selling_prices_sku_id",
                table: "sku_selling_prices",
                column: "sku_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sku_attributes_skus_stock_keeping_unit_id",
                table: "sku_attributes",
                column: "stock_keeping_unit_id",
                principalTable: "skus",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sku_attributes_skus_stock_keeping_unit_id",
                table: "sku_attributes");

            migrationBuilder.DropTable(
                name: "sku_selling_prices");

            migrationBuilder.DropTable(
                name: "price_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_skus",
                table: "skus");

            migrationBuilder.RenameTable(
                name: "skus",
                newName: "Skus");

            migrationBuilder.AddColumn<decimal>(
                name: "selling_price",
                table: "Skus",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skus",
                table: "Skus",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_sku_attributes_Skus_stock_keeping_unit_id",
                table: "sku_attributes",
                column: "stock_keeping_unit_id",
                principalTable: "Skus",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
