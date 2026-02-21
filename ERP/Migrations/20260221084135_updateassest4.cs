using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class updateassest4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PartName",
                table: "Assets");

            migrationBuilder.CreateTable(
                name: "AssetItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    PartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetItem_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetItem_AssetId",
                table: "AssetItem",
                column: "AssetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetItem");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Assets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartName",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
