using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class RoutePointTripCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_point_trip_trips_trip_id",
                schema: "public",
                table: "route_point_trip");

            migrationBuilder.AddForeignKey(
                name: "FK_route_point_trip_trips_trip_id",
                schema: "public",
                table: "route_point_trip",
                column: "trip_id",
                principalSchema: "public",
                principalTable: "trips",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_point_trip_trips_trip_id",
                schema: "public",
                table: "route_point_trip");

            migrationBuilder.AddForeignKey(
                name: "FK_route_point_trip_trips_trip_id",
                schema: "public",
                table: "route_point_trip",
                column: "trip_id",
                principalSchema: "public",
                principalTable: "trips",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
