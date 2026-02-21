using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class AssetManagementRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_AssestUsers_AssestUserId",
                table: "AssetHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_AssetProperties_AssetPropertyId",
                table: "AssetHistories");

            migrationBuilder.DropTable(
                name: "AssetProperties");

            migrationBuilder.DropIndex(
                name: "IX_AssetHistories_AssestUserId",
                table: "AssetHistories");

            migrationBuilder.DropIndex(
                name: "IX_AssetHistories_AssetPropertyId",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "AssestUserId",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "AssetPropertyId",
                table: "AssetHistories");

            migrationBuilder.RenameColumn(
                name: "FromUserId",
                table: "AssetHistories",
                newName: "FromUserId");

            migrationBuilder.RenameColumn(
                name: "ToUserId",
                table: "AssetHistories",
                newName: "ToUserId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentOwnerId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_CurrentOwnerId",
                table: "Assets",
                column: "CurrentOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetHistories_FromUserId",
                table: "AssetHistories",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetHistories_ToUserId",
                table: "AssetHistories",
                column: "ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AssestUsers_CurrentOwnerId",
                table: "Assets",
                column: "CurrentOwnerId",
                principalTable: "AssestUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_AssestUsers_FromUserId",
                table: "AssetHistories",
                column: "FromUserId",
                principalTable: "AssestUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_AssestUsers_ToUserId",
                table: "AssetHistories",
                column: "ToUserId",
                principalTable: "AssestUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssestUsers_CurrentOwnerId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_AssestUsers_FromUserId",
                table: "AssetHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_AssestUsers_ToUserId",
                table: "AssetHistories");

            migrationBuilder.DropIndex(
                name: "IX_Assets_CurrentOwnerId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_AssetHistories_FromUserId",
                table: "AssetHistories");

            migrationBuilder.DropIndex(
                name: "IX_AssetHistories_ToUserId",
                table: "AssetHistories");

            migrationBuilder.DropColumn(
                name: "CurrentOwnerId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "FromUserId",
                table: "AssetHistories",
                newName: "FromUserId");

            migrationBuilder.RenameColumn(
                name: "ToUserId",
                table: "AssetHistories",
                newName: "ToUserId");

            migrationBuilder.AddColumn<int>(
                name: "AssestUserId",
                table: "AssetHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssetPropertyId",
                table: "AssetHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssetProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    LastOwnerUserId = table.Column<int>(type: "int", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastAssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetProperties_AssestCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AssestCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetProperties_AssestUsers_LastOwnerUserId",
                        column: x => x.LastOwnerUserId,
                        principalTable: "AssestUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetProperties_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetHistories_AssestUserId",
                table: "AssetHistories",
                column: "AssestUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetHistories_AssetPropertyId",
                table: "AssetHistories",
                column: "AssetPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetProperties_AssetId",
                table: "AssetProperties",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetProperties_CategoryId",
                table: "AssetProperties",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetProperties_LastOwnerUserId",
                table: "AssetProperties",
                column: "LastOwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_AssestUsers_AssestUserId",
                table: "AssetHistories",
                column: "AssestUserId",
                principalTable: "AssestUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_AssetProperties_AssetPropertyId",
                table: "AssetHistories",
                column: "AssetPropertyId",
                principalTable: "AssetProperties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
