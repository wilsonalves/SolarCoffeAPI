using Microsoft.EntityFrameworkCore.Migrations;

namespace SolarCoffee.Data.Migrations
{
    public partial class _0815 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityOnHand",
                table: "ProductInventorySnapshots",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityOnHand",
                table: "ProductInventorySnapshots");
        }
    }
}
