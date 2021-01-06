using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddedCodeInTenantModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "tenants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tenants_code",
                table: "tenants",
                column: "code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tenants_code",
                table: "tenants");

            migrationBuilder.DropColumn(
                name: "code",
                table: "tenants");
        }
    }
}
