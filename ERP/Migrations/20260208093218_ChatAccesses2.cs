using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class AddChatAccessFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "ChatAccesses",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),
                  UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                  AllowedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                  IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                  CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_ChatAccesses", x => x.Id);
                  table.ForeignKey(
                      name: "FK_ChatAccesses_AspNetUsers_AllowedUserId",
                      column: x => x.AllowedUserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction);
                  table.ForeignKey(
                      name: "FK_ChatAccesses_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction);
              });

            migrationBuilder.CreateIndex(
                name: "IX_ChatAccesses_AllowedUserId",
                table: "ChatAccesses",
                column: "AllowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatAccesses_UserId_AllowedUserId",
                table: "ChatAccesses",
                columns: new[] { "UserId", "AllowedUserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "ChatAccesses");
        }
    }
}
