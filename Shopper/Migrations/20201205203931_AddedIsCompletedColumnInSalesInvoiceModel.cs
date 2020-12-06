using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddedIsCompletedColumnInSalesInvoiceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "sale_invoice_id",
                table: "sales",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.CreateTable(
                name: "sale_invoices",
                columns: table => new
                {
                    id = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    number = table.Column<string>(nullable: true),
                    amount = table.Column<ulong>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    is_completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sale_invoices", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sales_sale_invoice_id",
                table: "sales",
                column: "sale_invoice_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_sale_invoices_sale_invoice_id",
                table: "sales",
                column: "sale_invoice_id",
                principalTable: "sale_invoices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_sale_invoices_sale_invoice_id",
                table: "sales");

            migrationBuilder.DropTable(
                name: "sale_invoices");

            migrationBuilder.DropIndex(
                name: "IX_sales_sale_invoice_id",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "sale_invoice_id",
                table: "sales");
        }
    }
}
