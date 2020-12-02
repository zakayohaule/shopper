using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class ReplacedDecimalWithUintInAllDecimalFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "selling_price",
                table: "skus",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,2)");

            migrationBuilder.AlterColumn<uint>(
                name: "maximum_discount",
                table: "skus",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,2)");

            migrationBuilder.AlterColumn<uint>(
                name: "buying_price",
                table: "skus",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,2)");

            migrationBuilder.AlterColumn<uint>(
                name: "price",
                table: "sku_selling_prices",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "price",
                table: "sales",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(30,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "selling_price",
                table: "skus",
                type: "decimal(30,2)",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<decimal>(
                name: "maximum_discount",
                table: "skus",
                type: "decimal(30,2)",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<decimal>(
                name: "buying_price",
                table: "skus",
                type: "decimal(30,2)",
                nullable: false,
                oldClrType: typeof(uint));

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "sku_selling_prices",
                type: "decimal(30,2)",
                nullable: true,
                oldClrType: typeof(uint),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "sales",
                type: "decimal(30,2)",
                nullable: false,
                oldClrType: typeof(uint));
        }
    }
}
