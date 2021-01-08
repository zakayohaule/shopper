using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class RemovedUniqueIndexInSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_name",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_price_types_name",
                table: "price_types");

            migrationBuilder.DropIndex(
                name: "IX_attributes_name",
                table: "attributes");

            migrationBuilder.DropIndex(
                name: "IX_attribute_options_name",
                table: "attribute_options");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "products",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "price_types",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "attributes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "attribute_options",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "products",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "price_types",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "attributes",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "attribute_options",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_products_name",
                table: "products",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_price_types_name",
                table: "price_types",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_attributes_name",
                table: "attributes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_attribute_options_name",
                table: "attribute_options",
                column: "name",
                unique: true);
        }
    }
}
