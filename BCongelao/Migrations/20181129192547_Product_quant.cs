using Microsoft.EntityFrameworkCore.Migrations;

namespace BCongelao.Migrations
{
    public partial class Product_quant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "Product",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
