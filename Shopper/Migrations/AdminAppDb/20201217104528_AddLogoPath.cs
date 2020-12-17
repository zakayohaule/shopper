using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations.AdminAppDb
{
    public partial class AddLogoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "logo_path",
                table: "tenants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "logo_path",
                table: "tenants");
        }
    }
}
