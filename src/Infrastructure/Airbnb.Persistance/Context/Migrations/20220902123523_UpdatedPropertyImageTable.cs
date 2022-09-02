using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class UpdatedPropertyImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alternative",
                table: "PropertyImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alternative",
                table: "PropertyImages",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);
        }
    }
}
