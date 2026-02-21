using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class updateassest5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetItem_Assets_AssetId",
                table: "AssetItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetItem",
                table: "AssetItem");

            migrationBuilder.RenameTable(
                name: "AssetItem",
                newName: "AssetItems");

            migrationBuilder.RenameIndex(
                name: "IX_AssetItem_AssetId",
                table: "AssetItems",
                newName: "IX_AssetItems_AssetId");

            migrationBuilder.AlterColumn<string>(
                name: "PartName",
                table: "AssetItems",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AssetItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "AssetItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetItems",
                table: "AssetItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetItems_Assets_AssetId",
                table: "AssetItems",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetItems_Assets_AssetId",
                table: "AssetItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetItems",
                table: "AssetItems");

            migrationBuilder.RenameTable(
                name: "AssetItems",
                newName: "AssetItem");

            migrationBuilder.RenameIndex(
                name: "IX_AssetItems_AssetId",
                table: "AssetItem",
                newName: "IX_AssetItem_AssetId");

            migrationBuilder.AlterColumn<string>(
                name: "PartName",
                table: "AssetItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AssetItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "AssetItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetItem",
                table: "AssetItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetItem_Assets_AssetId",
                table: "AssetItem",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
