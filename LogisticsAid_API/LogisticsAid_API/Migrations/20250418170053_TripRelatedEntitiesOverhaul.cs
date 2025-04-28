using System;
using LogisticsAid_API.Entities.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class TripRelatedEntitiesOverhaul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_points_contact_info_contact_info_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_carriers_carrier_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_customers_customer_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_transport_transport_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropTable(
                name: "carriers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_trips_carrier_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_customer_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_route_points_contact_info_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropColumn(
                name: "carrier_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "customer_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "company_name",
                schema: "public",
                table: "transport");

            migrationBuilder.DropColumn(
                name: "trailer_license_plate",
                schema: "public",
                table: "transport");

            migrationBuilder.DropColumn(
                name: "truck_brand",
                schema: "public",
                table: "transport");

            migrationBuilder.DropColumn(
                name: "contact_info_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropColumn(
                name: "company_name",
                schema: "public",
                table: "drivers");

            migrationBuilder.RenameColumn(
                name: "transport_id",
                schema: "public",
                table: "trips",
                newName: "truck_id");

            migrationBuilder.RenameIndex(
                name: "IX_trips_transport_id",
                schema: "public",
                table: "trips",
                newName: "IX_trips_truck_id");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:gender", "female,male,special")
                .Annotation("Npgsql:Enum:route_point_type", "loading,unloading")
                .Annotation("Npgsql:Enum:transport_type", "trailer,truck")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .OldAnnotation("Npgsql:Enum:gender", "female,male,special")
                .OldAnnotation("Npgsql:Enum:route_point_type", "loading,unloading")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AddColumn<string>(
                name: "carrier_company_id",
                schema: "public",
                table: "trips",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "customer_company_id",
                schema: "public",
                table: "trips",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "trailer_id",
                schema: "public",
                table: "trips",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "brand",
                schema: "public",
                table: "transport",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "carrier_company_id",
                schema: "public",
                table: "transport",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<ETransportType>(
                name: "type",
                schema: "public",
                table: "transport",
                type: "transport_type",
                nullable: false,
                defaultValue: ETransportType.Truck);

            migrationBuilder.AlterColumn<string>(
                name: "company_name",
                schema: "public",
                table: "route_points",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "additional_info",
                schema: "public",
                table: "route_points",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "carrier_company_id",
                schema: "public",
                table: "drivers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "public",
                table: "contact_info",
                type: "character varying(254)",
                maxLength: 254,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(254)",
                oldMaxLength: 254,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "carrier_company_id",
                schema: "public",
                table: "contact_info",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "customer_company_id",
                schema: "public",
                table: "contact_info",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "street",
                schema: "public",
                table: "addresses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "province",
                schema: "public",
                table: "addresses",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "number",
                schema: "public",
                table: "addresses",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "country",
                schema: "public",
                table: "addresses",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "carrier_companies",
                schema: "public",
                columns: table => new
                {
                    company_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carrier_companies", x => x.company_name);
                });

            migrationBuilder.CreateTable(
                name: "customer_companies",
                schema: "public",
                columns: table => new
                {
                    company_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_companies", x => x.company_name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trips_carrier_company_id",
                schema: "public",
                table: "trips",
                column: "carrier_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_customer_company_id",
                schema: "public",
                table: "trips",
                column: "customer_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_trailer_id",
                schema: "public",
                table: "trips",
                column: "trailer_id");

            migrationBuilder.CreateIndex(
                name: "IX_transport_carrier_company_id",
                schema: "public",
                table: "transport",
                column: "carrier_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_drivers_carrier_company_id",
                schema: "public",
                table: "drivers",
                column: "carrier_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_info_carrier_company_id",
                schema: "public",
                table: "contact_info",
                column: "carrier_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_info_customer_company_id",
                schema: "public",
                table: "contact_info",
                column: "customer_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_info_email",
                schema: "public",
                table: "contact_info",
                column: "email",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_ContactInfo_SingleCompany",
                schema: "public",
                table: "contact_info",
                sql: "customer_company_id IS NULL OR carrier_company_id IS NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_info_carrier_companies_carrier_company_id",
                schema: "public",
                table: "contact_info",
                column: "carrier_company_id",
                principalSchema: "public",
                principalTable: "carrier_companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contact_info_customer_companies_customer_company_id",
                schema: "public",
                table: "contact_info",
                column: "customer_company_id",
                principalSchema: "public",
                principalTable: "customer_companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_carrier_companies_carrier_company_id",
                schema: "public",
                table: "drivers",
                column: "carrier_company_id",
                principalSchema: "public",
                principalTable: "carrier_companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transport_carrier_companies_carrier_company_id",
                schema: "public",
                table: "transport",
                column: "carrier_company_id",
                principalSchema: "public",
                principalTable: "carrier_companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_carrier_companies_carrier_company_id",
                schema: "public",
                table: "trips",
                column: "carrier_company_id",
                principalSchema: "public",
                principalTable: "carrier_companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_customer_companies_customer_company_id",
                schema: "public",
                table: "trips",
                column: "customer_company_id",
                principalSchema: "public",
                principalTable: "customer_companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_transport_trailer_id",
                schema: "public",
                table: "trips",
                column: "trailer_id",
                principalSchema: "public",
                principalTable: "transport",
                principalColumn: "license_plate",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_transport_truck_id",
                schema: "public",
                table: "trips",
                column: "truck_id",
                principalSchema: "public",
                principalTable: "transport",
                principalColumn: "license_plate",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_info_carrier_companies_carrier_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropForeignKey(
                name: "FK_contact_info_customer_companies_customer_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropForeignKey(
                name: "FK_drivers_carrier_companies_carrier_company_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_transport_carrier_companies_carrier_company_id",
                schema: "public",
                table: "transport");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_carrier_companies_carrier_company_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_customer_companies_customer_company_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_transport_trailer_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_transport_truck_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropTable(
                name: "carrier_companies",
                schema: "public");

            migrationBuilder.DropTable(
                name: "customer_companies",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_trips_carrier_company_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_customer_company_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_trailer_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_transport_carrier_company_id",
                schema: "public",
                table: "transport");

            migrationBuilder.DropIndex(
                name: "IX_drivers_carrier_company_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropIndex(
                name: "IX_contact_info_carrier_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropIndex(
                name: "IX_contact_info_customer_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropIndex(
                name: "IX_contact_info_email",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ContactInfo_SingleCompany",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropColumn(
                name: "carrier_company_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "customer_company_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "trailer_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "brand",
                schema: "public",
                table: "transport");

            migrationBuilder.DropColumn(
                name: "carrier_company_id",
                schema: "public",
                table: "transport");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "public",
                table: "transport");

            migrationBuilder.DropColumn(
                name: "additional_info",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropColumn(
                name: "carrier_company_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropColumn(
                name: "carrier_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropColumn(
                name: "customer_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.RenameColumn(
                name: "truck_id",
                schema: "public",
                table: "trips",
                newName: "transport_id");

            migrationBuilder.RenameIndex(
                name: "IX_trips_truck_id",
                schema: "public",
                table: "trips",
                newName: "IX_trips_transport_id");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:gender", "female,male,special")
                .Annotation("Npgsql:Enum:route_point_type", "loading,unloading")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,")
                .OldAnnotation("Npgsql:Enum:gender", "female,male,special")
                .OldAnnotation("Npgsql:Enum:route_point_type", "loading,unloading")
                .OldAnnotation("Npgsql:Enum:transport_type", "trailer,truck")
                .OldAnnotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.AddColumn<Guid>(
                name: "carrier_id",
                schema: "public",
                table: "trips",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "customer_id",
                schema: "public",
                table: "trips",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "company_name",
                schema: "public",
                table: "transport",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "trailer_license_plate",
                schema: "public",
                table: "transport",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "truck_brand",
                schema: "public",
                table: "transport",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "company_name",
                schema: "public",
                table: "route_points",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "contact_info_id",
                schema: "public",
                table: "route_points",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "company_name",
                schema: "public",
                table: "drivers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "public",
                table: "contact_info",
                type: "character varying(254)",
                maxLength: 254,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(254)",
                oldMaxLength: 254);

            migrationBuilder.AlterColumn<string>(
                name: "street",
                schema: "public",
                table: "addresses",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "province",
                schema: "public",
                table: "addresses",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "number",
                schema: "public",
                table: "addresses",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "country",
                schema: "public",
                table: "addresses",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

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
                        name: "FK_customers_contact_info_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "public",
                        principalTable: "contact_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_route_points_contact_info_id",
                schema: "public",
                table: "route_points",
                column: "contact_info_id");

            migrationBuilder.AddForeignKey(
                name: "FK_route_points_contact_info_contact_info_id",
                schema: "public",
                table: "route_points",
                column: "contact_info_id",
                principalSchema: "public",
                principalTable: "contact_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_trips_transport_transport_id",
                schema: "public",
                table: "trips",
                column: "transport_id",
                principalSchema: "public",
                principalTable: "transport",
                principalColumn: "license_plate",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
