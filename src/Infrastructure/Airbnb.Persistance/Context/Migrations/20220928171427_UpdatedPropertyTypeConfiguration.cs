using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class UpdatedPropertyTypeConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyGroups_Image",
                table: "PropertyGroups");

            migrationBuilder.DropIndex(
                name: "IX_PropertTypes_Description",
                table: "PropertTypes");

            migrationBuilder.DropIndex(
                name: "IX_PropertTypes_Icon",
                table: "PropertTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PropertyGroups_Image",
                table: "PropertyGroups",
                column: "Image",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertTypes_Description",
                table: "PropertTypes",
                column: "Description",
                unique: true,
                filter: "[Description] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertTypes_Icon",
                table: "PropertTypes",
                column: "Icon",
                unique: true);
        }
    }
}
