using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class AddressIndexUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_points_contact_info_contact_info_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_country_province_city_street_number",
                schema: "public",
                table: "addresses",
                columns: new[] { "country", "province", "city", "street", "number" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_route_points_contact_info_contact_info_id",
                schema: "public",
                table: "route_points",
                column: "contact_info_id",
                principalSchema: "public",
                principalTable: "contact_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_route_points_contact_info_contact_info_id",
                schema: "public",
                table: "route_points");

            migrationBuilder.DropIndex(
                name: "IX_addresses_country_province_city_street_number",
                schema: "public",
                table: "addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_route_points_contact_info_contact_info_id",
                schema: "public",
                table: "route_points",
                column: "contact_info_id",
                principalSchema: "public",
                principalTable: "contact_info",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
