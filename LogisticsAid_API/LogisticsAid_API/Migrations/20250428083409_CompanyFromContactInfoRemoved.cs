using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class CompanyFromContactInfoRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_info_carrier_companies_carrier_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropForeignKey(
                name: "FK_contact_info_customer_companies_customer_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropIndex(
                name: "IX_contact_info_carrier_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropIndex(
                name: "IX_contact_info_customer_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropColumn(
                name: "carrier_company_id",
                schema: "public",
                table: "contact_info");

            migrationBuilder.DropColumn(
                name: "customer_company_id",
                schema: "public",
                table: "contact_info");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
