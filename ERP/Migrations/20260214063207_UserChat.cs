using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class UserChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniqueToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestVerificationCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    AttemptCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestVerificationCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestChatAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestUserId = table.Column<int>(type: "int", nullable: false),
                    AllowedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GrantedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestChatAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestChatAccesses_AspNetUsers_AllowedUserId",
                        column: x => x.AllowedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestChatAccesses_GuestUsers_GuestUserId",
                        column: x => x.GuestUserId,
                        principalTable: "GuestUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuestChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuestSenderId = table.Column<int>(type: "int", nullable: true),
                    UserSenderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GuestReceiverId = table.Column<int>(type: "int", nullable: true),
                    UserReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_GuestChatAccesses_AllowedUserId",
                table: "GuestChatAccesses",
                column: "AllowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestChatAccesses_GuestUserId",
                table: "GuestChatAccesses",
                column: "GuestUserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_GuestUsers_PhoneNumber",
                table: "GuestUsers",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_GuestUsers_UniqueToken",
                table: "GuestUsers",
                column: "UniqueToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuestVerificationCodes_PhoneNumber",
                table: "GuestVerificationCodes",
                column: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestChatAccesses");

            migrationBuilder.DropTable(
                name: "GuestChatMessages");

            migrationBuilder.DropTable(
                name: "GuestVerificationCodes");

            migrationBuilder.DropTable(
                name: "GuestUsers");
        }
    }
}
