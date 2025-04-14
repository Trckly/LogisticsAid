using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsAid_API.Migrations
{
    /// <inheritdoc />
    public partial class TripPriceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                schema: "public",
                table: "trips",
                newName: "customer_price");

            migrationBuilder.AddColumn<decimal>(
                name: "carrier_price",
                schema: "public",
                table: "trips",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "carrier_price",
                schema: "public",
                table: "trips");

            migrationBuilder.RenameColumn(
                name: "customer_price",
                schema: "public",
                table: "trips",
                newName: "price");
        }
    }
}
