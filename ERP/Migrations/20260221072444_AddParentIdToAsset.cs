using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class AddParentIdToAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Assets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ParentId",
                table: "Assets",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Assets_ParentId",
                table: "Assets",
                column: "ParentId",
                principalTable: "Assets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Assets_ParentId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ParentId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Assets");
        }
    }
}
