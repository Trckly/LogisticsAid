using System;
using LogisticsAid_API.Entities.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:gender", "female,male,special")
                .Annotation("Npgsql:Enum:route_point_type", "loading,unloading")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "addresses",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    province = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: true),
                    longitude = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "public",
                columns: table => new
                {
                    company_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.company_name);
                });

            migrationBuilder.CreateTable(
                name: "contact_info",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transport",
                schema: "public",
                columns: table => new
                {
                    licence_plate = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    truck_brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    trailer_licence_plate = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    company_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport", x => x.licence_plate);
                    table.ForeignKey(
                        name: "FK_transport_companies_company_name",
                        column: x => x.company_name,
                        principalSchema: "public",
                        principalTable: "companies",
                        principalColumn: "company_name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carriers",
                schema: "public",
                columns: table => new
                {
                    contact_id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carriers", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_carriers_companies_company_name",
                        column: x => x.company_name,
                        principalSchema: "public",
                        principalTable: "companies",
                        principalColumn: "company_name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carriers_contact_info_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "public",
                        principalTable: "contact_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "public",
                columns: table => new
                {
                    contact_id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_customers_companies_company_name",
                        column: x => x.company_name,
                        principalSchema: "public",
                        principalTable: "companies",
                        principalColumn: "company_name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customers_contact_info_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "public",
                        principalTable: "contact_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "drivers",
                schema: "public",
                columns: table => new
                {
                    contact_id = table.Column<Guid>(type: "uuid", nullable: false),
                    licence = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    company_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drivers", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_drivers_companies_company_name",
                        column: x => x.company_name,
                        principalSchema: "public",
                        principalTable: "companies",
                        principalColumn: "company_name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_drivers_contact_info_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "public",
                        principalTable: "contact_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "logisticians",
                schema: "public",
                columns: table => new
                {
                    contact_id = table.Column<Guid>(type: "uuid", nullable: false),
                    password_salt = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    admin_privileges = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logisticians", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_logisticians_contact_info_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "public",
                        principalTable: "contact_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
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

            migrationBuilder.CreateTable(
                name: "route_points",
                schema: "public",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<ERoutePointType>(type: "route_point_type", nullable: false),
                    sequence = table.Column<int>(type: "integer", nullable: false),
                    contact_info_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route_points", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_route_points_addresses_address_id",
                        column: x => x.address_id,
                        principalSchema: "public",
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_route_points_contact_info_contact_info_id",
                        column: x => x.contact_info_id,
                        principalSchema: "public",
                        principalTable: "contact_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_route_points_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "public",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carriers_company_name",
                schema: "public",
                table: "carriers",
                column: "company_name");

            migrationBuilder.CreateIndex(
                name: "IX_contact_info_phone",
                schema: "public",
                table: "contact_info",
                column: "phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customers_company_name",
                schema: "public",
                table: "customers",
                column: "company_name");

            migrationBuilder.CreateIndex(
                name: "IX_drivers_company_name",
                schema: "public",
                table: "drivers",
                column: "company_name");

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

            migrationBuilder.CreateIndex(
                name: "IX_route_points_address_id",
                schema: "public",
                table: "route_points",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_route_points_contact_info_id",
                schema: "public",
                table: "route_points",
                column: "contact_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_transport_company_name",
                schema: "public",
                table: "transport",
                column: "company_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "route_points",
                schema: "public");

            migrationBuilder.DropTable(
                name: "addresses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "public");

            migrationBuilder.DropTable(
                name: "carriers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "drivers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "logisticians",
                schema: "public");

            migrationBuilder.DropTable(
                name: "transport",
                schema: "public");

            migrationBuilder.DropTable(
                name: "contact_info",
                schema: "public");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "public");
        }
    }
}
