using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddUserIdInSaleInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "sale_invoices",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_sale_invoices_user_id",
                table: "sale_invoices",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sale_invoices_users_user_id",
                table: "sale_invoices",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sale_invoices_users_user_id",
                table: "sale_invoices");

            migrationBuilder.DropIndex(
                name: "IX_sale_invoices_user_id",
                table: "sale_invoices");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "sale_invoices");
        }
    }
}
