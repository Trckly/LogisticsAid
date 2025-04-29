using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class AddAuxilaryTables_CICC_CICC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "carrier_company_id",
                schema: "public",
                table: "transport",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "carrier_company_id",
                schema: "public",
                table: "drivers",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "contact_info_carrier_company",
                schema: "public",
                columns: table => new
                {
                    contact_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    carrier_company_id = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_info_carrier_company", x => new { x.contact_info_id, x.carrier_company_id });
                    table.ForeignKey(
                        name: "FK_contact_info_carrier_company_carrier_companies_carrier_comp~",
                        column: x => x.carrier_company_id,
                        principalSchema: "public",
                        principalTable: "carrier_companies",
                        principalColumn: "company_name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_contact_info_carrier_company_contact_info_contact_info_id",
                        column: x => x.contact_info_id,
                        principalSchema: "public",
                        principalTable: "contact_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "contact_info_customer_company",
                schema: "public",
                columns: table => new
                {
                    contact_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_company_id = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_info_customer_company", x => new { x.contact_info_id, x.customer_company_id });
                    table.ForeignKey(
                        name: "FK_contact_info_customer_company_contact_info_contact_info_id",
                        column: x => x.contact_info_id,
                        principalSchema: "public",
                        principalTable: "contact_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_contact_info_customer_company_customer_companies_customer_c~",
                        column: x => x.customer_company_id,
                        principalSchema: "public",
                        principalTable: "customer_companies",
                        principalColumn: "company_name",
                        onDelete: ReferentialAction.Restrict);
                });
            
            migrationBuilder.AddForeignKey(
                name: "FK_drivers_carrier_companies_carrier_company_id",
                schema: "public",
                table: "drivers",
                column: "carrier_company_id",
                principalSchema: "public",
                principalTable: "carrier_companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transport_carrier_companies_carrier_company_id",
                schema: "public",
                table: "transport",
                column: "carrier_company_id",
                principalSchema: "public",
                principalTable: "carrier_companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drivers_carrier_companies_carrier_company_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_transport_carrier_companies_carrier_company_id",
                schema: "public",
                table: "transport");

            migrationBuilder.DropTable(
                name: "contact_info_carrier_company",
                schema: "public");

            migrationBuilder.DropTable(
                name: "contact_info_customer_company",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_transport_carrier_company_id",
                schema: "public",
                table: "transport");

            migrationBuilder.DropIndex(
                name: "IX_drivers_carrier_company_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropColumn(
                name: "carrier_company_id",
                schema: "public",
                table: "transport");

            migrationBuilder.DropColumn(
                name: "carrier_company_id",
                schema: "public",
                table: "drivers");
        }
    }
}
