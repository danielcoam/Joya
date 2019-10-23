using Microsoft.EntityFrameworkCore.Migrations;

namespace Joya.Migrations
{
    public partial class addstoremodelfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "latitude",
                table: "Store",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "longitude",
                table: "Store",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "streetName",
                table: "Store",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latitude",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "streetName",
                table: "Store");
        }
    }
}
