using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class QuickFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_points_orders_order_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "public");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birth_date",
                schema: "public",
                table: "contact_info",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "trips",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    readable_id = table.Column<long>(type: "bigint", nullable: false),
                    logistician_id = table.Column<Guid>(type: "uuid", nullable: false),
                    carrier_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    transport_id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trips", x => x.id);
                    table.ForeignKey(
                        name: "FK_trips_carriers_carrier_id",
                        column: x => x.carrier_id,
                        principalSchema: "public",
                        principalTable: "carriers",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trips_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "public",
                        principalTable: "customers",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trips_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "public",
                        principalTable: "drivers",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trips_logisticians_logistician_id",
                        column: x => x.logistician_id,
                        principalSchema: "public",
                        principalTable: "logisticians",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trips_transport_transport_id",
                        column: x => x.transport_id,
                        principalSchema: "public",
                        principalTable: "transport",
                        principalColumn: "licence_plate",
                        onDelete: ReferentialAction.Cascade);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_points_trips_order_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropTable(
                name: "trips",
                schema: "public");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "birth_date",
                schema: "public",
                table: "contact_info",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    carrier_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    driver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    logistician_id = table.Column<Guid>(type: "uuid", nullable: false),
                    transport_id = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    readable_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_carriers_carrier_id",
                        column: x => x.carrier_id,
                        principalSchema: "public",
                        principalTable: "carriers",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "public",
                        principalTable: "customers",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "public",
                        principalTable: "drivers",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_logisticians_logistician_id",
                        column: x => x.logistician_id,
                        principalSchema: "public",
                        principalTable: "logisticians",
                        principalColumn: "contact_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_transport_transport_id",
                        column: x => x.transport_id,
                        principalSchema: "public",
                        principalTable: "transport",
                        principalColumn: "licence_plate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_carrier_id",
                schema: "public",
                table: "orders",
                column: "carrier_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                schema: "public",
                table: "orders",
                column: "customer_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_driver_id",
                schema: "public",
                table: "orders",
                column: "driver_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_logistician_id",
                schema: "public",
                table: "orders",
                column: "logistician_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_transport_id",
                schema: "public",
                table: "orders",
                column: "transport_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_route_points_orders_order_id",
                schema: "public",
                table: "route_points",
                column: "order_id",
                principalSchema: "public",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
