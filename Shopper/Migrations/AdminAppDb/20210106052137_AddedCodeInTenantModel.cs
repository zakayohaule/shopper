using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations.AdminAppDb
{
    public partial class AddedCodeInTenantModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "tenants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "tenants");
        }
    }
}
