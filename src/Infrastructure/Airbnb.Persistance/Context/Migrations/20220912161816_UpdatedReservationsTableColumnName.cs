using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class UpdatedReservationsTableColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Reservations",
                newName: "PricePerDay");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerDay",
                table: "Reservations",
                newName: "Price");
        }
    }
}
