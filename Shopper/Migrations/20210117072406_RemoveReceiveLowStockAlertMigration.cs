using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class RemoveReceiveLowStockAlertMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "receive_low_alert",
                table: "skus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "receive_low_alert",
                table: "skus",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
