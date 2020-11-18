using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddedInitialProductAndSkuRelatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skus",
                columns: table => new
                {
                    id = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    product_id = table.Column<uint>(nullable: false),
                    buying_price = table.Column<decimal>(nullable: false),
                    selling_price = table.Column<decimal>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    remaining_quantity = table.Column<int>(nullable: false),
                    maximum_discount = table.Column<decimal>(nullable: false),
                    is_on_sale = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_groups",
                columns: table => new
                {
                    id = table.Column<ushort>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "attribute_options",
                columns: table => new
                {
                    id = table.Column<ushort>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    name = table.Column<string>(nullable: true),
                    attribute_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attribute_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_attribute_options_attributes_attribute_id",
                        column: x => x.attribute_id,
                        principalTable: "attributes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_categories",
                columns: table => new
                {
                    id = table.Column<ushort>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    name = table.Column<string>(nullable: true),
                    product_group_id = table.Column<ushort>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_categories_product_groups_product_group_id",
                        column: x => x.product_group_id,
                        principalTable: "product_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sku_attributes",
                columns: table => new
                {
                    id = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    attribute_option_id = table.Column<ushort>(nullable: false),
                    stock_keeping_unit_id = table.Column<ulong>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sku_attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_sku_attributes_attribute_options_attribute_option_id",
                        column: x => x.attribute_option_id,
                        principalTable: "attribute_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sku_attributes_Skus_stock_keeping_unit_id",
                        column: x => x.stock_keeping_unit_id,
                        principalTable: "Skus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    name = table.Column<string>(nullable: true),
                    image_path = table.Column<string>(nullable: true),
                    slug = table.Column<string>(nullable: true),
                    product_category_id = table.Column<ushort>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_product_categories_product_category_id",
                        column: x => x.product_category_id,
                        principalTable: "product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_attribute_options",
                columns: table => new
                {
                    attribute_option_id = table.Column<ushort>(nullable: false),
                    product_id = table.Column<uint>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_attribute_options", x => new { x.product_id, x.attribute_option_id });
                    table.ForeignKey(
                        name: "FK_product_attribute_options_attribute_options_attribute_option~",
                        column: x => x.attribute_option_id,
                        principalTable: "attribute_options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_attribute_options_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attribute_options_attribute_id",
                table: "attribute_options",
                column: "attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_attribute_options_name",
                table: "attribute_options",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_attributes_name",
                table: "attributes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_attribute_options_attribute_option_id",
                table: "product_attribute_options",
                column: "attribute_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_categories_name",
                table: "product_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_categories_product_group_id",
                table: "product_categories",
                column: "product_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_groups_name",
                table: "product_groups",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_name",
                table: "products",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_product_category_id",
                table: "products",
                column: "product_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_sku_attributes_attribute_option_id",
                table: "sku_attributes",
                column: "attribute_option_id");

            migrationBuilder.CreateIndex(
                name: "IX_sku_attributes_stock_keeping_unit_id",
                table: "sku_attributes",
                column: "stock_keeping_unit_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_attribute_options");

            migrationBuilder.DropTable(
                name: "sku_attributes");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "attribute_options");

            migrationBuilder.DropTable(
                name: "Skus");

            migrationBuilder.DropTable(
                name: "product_categories");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "product_groups");
        }
    }
}
