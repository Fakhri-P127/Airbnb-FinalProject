using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class DjKhaledANOTHERONE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultCount",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ChildrenCount",
                table: "Properties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "AdultCount",
                table: "Properties",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ChildrenCount",
                table: "Properties",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
