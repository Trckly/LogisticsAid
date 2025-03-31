using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class ReworkCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carriers_companies_company_name",
                schema: "public",
                table: "carriers");

            migrationBuilder.DropForeignKey(
                name: "FK_customers_companies_company_name",
                schema: "public",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "FK_drivers_companies_company_name",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_transport_companies_company_name",
                schema: "public",
                table: "transport");

            migrationBuilder.DropIndex(
                name: "IX_transport_company_name",
                schema: "public",
                table: "transport");

            migrationBuilder.DropIndex(
                name: "IX_drivers_company_name",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropIndex(
                name: "IX_customers_company_name",
                schema: "public",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_carriers_company_name",
                schema: "public",
                table: "carriers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_transport_company_name",
                schema: "public",
                table: "transport",
                column: "company_name");

            migrationBuilder.CreateIndex(
                name: "IX_drivers_company_name",
                schema: "public",
                table: "drivers",
                column: "company_name");

            migrationBuilder.CreateIndex(
                name: "IX_customers_company_name",
                schema: "public",
                table: "customers",
                column: "company_name");

            migrationBuilder.CreateIndex(
                name: "IX_carriers_company_name",
                schema: "public",
                table: "carriers",
                column: "company_name");

            migrationBuilder.AddForeignKey(
                name: "FK_carriers_companies_company_name",
                schema: "public",
                table: "carriers",
                column: "company_name",
                principalSchema: "public",
                principalTable: "companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_customers_companies_company_name",
                schema: "public",
                table: "customers",
                column: "company_name",
                principalSchema: "public",
                principalTable: "companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_companies_company_name",
                schema: "public",
                table: "drivers",
                column: "company_name",
                principalSchema: "public",
                principalTable: "companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transport_companies_company_name",
                schema: "public",
                table: "transport",
                column: "company_name",
                principalSchema: "public",
                principalTable: "companies",
                principalColumn: "company_name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
