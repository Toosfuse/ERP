using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class guard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRoutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RouteStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceRouteId = table.Column<int>(type: "int", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteStops_ServiceRoutes_ServiceRouteId",
                        column: x => x.ServiceRouteId,
                        principalTable: "ServiceRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceRouteId = table.Column<int>(type: "int", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    DepartureTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleLogs_ServiceRoutes_ServiceRouteId",
                        column: x => x.ServiceRouteId,
                        principalTable: "ServiceRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_ServiceRouteId",
                table: "RouteStops",
                column: "ServiceRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleLogs_ServiceRouteId",
                table: "VehicleLogs",
                column: "ServiceRouteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteStops");

            migrationBuilder.DropTable(
                name: "VehicleLogs");

            migrationBuilder.DropTable(
                name: "ServiceRoutes");
        }
    }
}
