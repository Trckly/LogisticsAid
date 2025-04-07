using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTripFKRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_trips_carrier_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_customer_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_driver_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_logistician_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_transport_id",
                schema: "public",
                table: "trips");

            migrationBuilder.CreateIndex(
                name: "IX_trips_carrier_id",
                schema: "public",
                table: "trips",
                column: "carrier_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_customer_id",
                schema: "public",
                table: "trips",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_driver_id",
                schema: "public",
                table: "trips",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_logistician_id",
                schema: "public",
                table: "trips",
                column: "logistician_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_readable_id",
                schema: "public",
                table: "trips",
                column: "readable_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trips_transport_id",
                schema: "public",
                table: "trips",
                column: "transport_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_trips_carrier_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_customer_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_driver_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_logistician_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_readable_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_transport_id",
                schema: "public",
                table: "trips");

            migrationBuilder.CreateIndex(
                name: "IX_trips_carrier_id",
                schema: "public",
                table: "trips",
                column: "carrier_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trips_customer_id",
                schema: "public",
                table: "trips",
                column: "customer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trips_driver_id",
                schema: "public",
                table: "trips",
                column: "driver_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trips_logistician_id",
                schema: "public",
                table: "trips",
                column: "logistician_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trips_transport_id",
                schema: "public",
                table: "trips",
                column: "transport_id",
                unique: true);
        }
    }
}
