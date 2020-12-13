using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopper.Migrations
{
    public partial class AddedExpenditureModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "expenditure_types",
                columns: table => new
                {
                    id = table.Column<ushort>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenditure_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "expenditures",
                columns: table => new
                {
                    id = table.Column<ulong>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    amount = table.Column<uint>(nullable: false),
                    expenditure_date = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    expenditure_type_id = table.Column<ushort>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenditures", x => x.id);
                    table.ForeignKey(
                        name: "FK_expenditures_expenditure_types_expenditure_type_id",
                        column: x => x.expenditure_type_id,
                        principalTable: "expenditure_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_expenditures_expenditure_type_id",
                table: "expenditures",
                column: "expenditure_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expenditures");

            migrationBuilder.DropTable(
                name: "expenditure_types");
        }
    }
}
