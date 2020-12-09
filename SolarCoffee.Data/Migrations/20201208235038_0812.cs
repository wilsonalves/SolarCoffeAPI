using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SolarCoffee.Data.Migrations
{
    public partial class _0812 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firsname",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityOnHand",
                table: "ProductInventorySnapshots",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "QuantityOnHand",
                table: "ProductInventorySnapshots",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastName",
                table: "Customers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Firsname",
                table: "Customers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
