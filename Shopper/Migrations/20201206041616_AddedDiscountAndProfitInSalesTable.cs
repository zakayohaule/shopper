using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddedDiscountAndProfitInSalesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "discount",
                table: "sales",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<long>(
                name: "profit",
                table: "sales",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discount",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "profit",
                table: "sales");
        }
    }
}
