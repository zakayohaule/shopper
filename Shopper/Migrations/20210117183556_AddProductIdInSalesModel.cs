using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddProductIdInSalesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "product_id",
                table: "sales",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_product_id",
                table: "sales",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_products_product_id",
                table: "sales",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_products_product_id",
                table: "sales");

            migrationBuilder.DropIndex(
                name: "IX_sales_product_id",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "product_id",
                table: "sales");
        }
    }
}
