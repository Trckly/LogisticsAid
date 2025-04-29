using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class CircularDependenciesRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drivers_carrier_companies_carrier_company_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_transport_carrier_companies_carrier_company_id",
                schema: "public",
                table: "transport");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "carrier_company_id",
                schema: "public",
                table: "transport",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "carrier_company_id",
                schema: "public",
                table: "drivers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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
        }
    }
}
