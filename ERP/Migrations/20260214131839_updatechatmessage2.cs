using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class updatechatmessage2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestChatMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestReceiverId = table.Column<int>(type: "int", nullable: true),
                    GuestSenderId = table.Column<int>(type: "int", nullable: true),
                    UserReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserSenderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AttachmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestChatMessages_AspNetUsers_UserReceiverId",
                        column: x => x.UserReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestChatMessages_AspNetUsers_UserSenderId",
                        column: x => x.UserSenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestChatMessages_GuestUsers_GuestReceiverId",
                        column: x => x.GuestReceiverId,
                        principalTable: "GuestUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestChatMessages_GuestUsers_GuestSenderId",
                        column: x => x.GuestSenderId,
                        principalTable: "GuestUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestChatMessages_GuestReceiverId",
                table: "GuestChatMessages",
                column: "GuestReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestChatMessages_GuestSenderId",
                table: "GuestChatMessages",
                column: "GuestSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestChatMessages_UserReceiverId",
                table: "GuestChatMessages",
                column: "UserReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestChatMessages_UserSenderId",
                table: "GuestChatMessages",
                column: "UserSenderId");
        }
    }
}
