using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class TripDeleteBehaviorUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trips_carriers_carrier_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_customers_customer_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_drivers_driver_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_logisticians_logistician_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_transport_transport_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_route_points",
                schema: "public",
                table: "route_points");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "public",
                table: "route_points",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_route_points",
                schema: "public",
                table: "route_points",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_route_points_order_id",
                schema: "public",
                table: "route_points",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_trips_carriers_carrier_id",
                schema: "public",
                table: "trips",
                column: "carrier_id",
                principalSchema: "public",
                principalTable: "carriers",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_customers_customer_id",
                schema: "public",
                table: "trips",
                column: "customer_id",
                principalSchema: "public",
                principalTable: "customers",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_drivers_driver_id",
                schema: "public",
                table: "trips",
                column: "driver_id",
                principalSchema: "public",
                principalTable: "drivers",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_logisticians_logistician_id",
                schema: "public",
                table: "trips",
                column: "logistician_id",
                principalSchema: "public",
                principalTable: "logisticians",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_transport_transport_id",
                schema: "public",
                table: "trips",
                column: "transport_id",
                principalSchema: "public",
                principalTable: "transport",
                principalColumn: "licence_plate",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trips_carriers_carrier_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_customers_customer_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_drivers_driver_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_logisticians_logistician_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_transport_transport_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_route_points",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropIndex(
                name: "IX_route_points_order_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "public",
                table: "route_points");

            migrationBuilder.AddPrimaryKey(
                name: "PK_route_points",
                schema: "public",
                table: "route_points",
                column: "order_id");

            migrationBuilder.AddForeignKey(
                name: "FK_trips_carriers_carrier_id",
                schema: "public",
                table: "trips",
                column: "carrier_id",
                principalSchema: "public",
                principalTable: "carriers",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_customers_customer_id",
                schema: "public",
                table: "trips",
                column: "customer_id",
                principalSchema: "public",
                principalTable: "customers",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_drivers_driver_id",
                schema: "public",
                table: "trips",
                column: "driver_id",
                principalSchema: "public",
                principalTable: "drivers",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_logisticians_logistician_id",
                schema: "public",
                table: "trips",
                column: "logistician_id",
                principalSchema: "public",
                principalTable: "logisticians",
                principalColumn: "contact_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_transport_transport_id",
                schema: "public",
                table: "trips",
                column: "transport_id",
                principalSchema: "public",
                principalTable: "transport",
                principalColumn: "licence_plate",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
