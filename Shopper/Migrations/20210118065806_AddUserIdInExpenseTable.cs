using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddUserIdInExpenseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "expenditures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_expenditures_user_id",
                table: "expenditures",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_expenditures_users_user_id",
                table: "expenditures",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expenditures_users_user_id",
                table: "expenditures");

            migrationBuilder.DropIndex(
                name: "IX_expenditures_user_id",
                table: "expenditures");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "expenditures");
        }
    }
}
