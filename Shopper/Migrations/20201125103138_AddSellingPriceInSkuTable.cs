using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddSellingPriceInSkuTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "selling_price",
                table: "skus",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "products",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "product_groups",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "product_categories",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "price_types",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "attributes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "attribute_options",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_skus_product_id",
                table: "skus",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_skus_products_product_id",
                table: "skus",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_skus_products_product_id",
                table: "skus");

            migrationBuilder.DropIndex(
                name: "IX_skus_product_id",
                table: "skus");

            migrationBuilder.DropColumn(
                name: "selling_price",
                table: "skus");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "products",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "product_groups",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "product_categories",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "price_types",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "attributes",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "attribute_options",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
