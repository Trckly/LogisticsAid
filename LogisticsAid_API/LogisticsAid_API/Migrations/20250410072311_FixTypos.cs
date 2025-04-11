using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class FixTypos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "trailer_licence_plate",
                schema: "public",
                table: "transport",
                newName: "trailer_license_plate");

            migrationBuilder.RenameColumn(
                name: "licence_plate",
                schema: "public",
                table: "transport",
                newName: "license_plate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "trailer_license_plate",
                schema: "public",
                table: "transport",
                newName: "trailer_licence_plate");

            migrationBuilder.RenameColumn(
                name: "license_plate",
                schema: "public",
                table: "transport",
                newName: "licence_plate");
        }
    }
}
