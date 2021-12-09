using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddSellerIdInSalesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "seller_id",
                table: "sales",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_sales_seller_id",
                table: "sales",
                column: "seller_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_users_seller_id",
                table: "sales",
                column: "seller_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_users_seller_id",
                table: "sales");

            migrationBuilder.DropIndex(
                name: "IX_sales_seller_id",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "seller_id",
                table: "sales");
        }
    }
}
