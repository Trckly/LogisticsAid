using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class RoutePointTrip_MTM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_points_trips_order_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropIndex(
                name: "IX_route_points_order_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropColumn(
                name: "order_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.CreateTable(
                name: "route_point_trip",
                schema: "public",
                columns: table => new
                {
                    route_point_id = table.Column<Guid>(type: "uuid", nullable: false),
                    trip_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_point_trip", x => new { x.route_point_id, x.trip_id });
                    table.ForeignKey(
                        name: "FK_route_point_trip_route_points_route_point_id",
                        column: x => x.route_point_id,
                        principalSchema: "public",
                        principalTable: "route_points",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_route_point_trip_trips_trip_id",
                        column: x => x.trip_id,
                        principalSchema: "public",
                        principalTable: "trips",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_route_point_trip_trip_id",
                schema: "public",
                table: "route_point_trip",
                column: "trip_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "route_point_trip",
                schema: "public");

            migrationBuilder.AddColumn<Guid>(
                name: "order_id",
                schema: "public",
                table: "route_points",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_route_points_order_id",
                schema: "public",
                table: "route_points",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_route_points_trips_order_id",
                schema: "public",
                table: "route_points",
                column: "order_id",
                principalSchema: "public",
                principalTable: "trips",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
