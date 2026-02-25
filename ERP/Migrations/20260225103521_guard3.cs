using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class guard3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Items",
                table: "RouteStops");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "RouteStops",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "RouteStops");

            migrationBuilder.AddColumn<string>(
                name: "Items",
                table: "RouteStops",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
