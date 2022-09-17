using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class ChangedBaseEntityPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "GuestReviews");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PropertyReviews",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PropertyImages",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PropertyGroups",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PropertyAmenities",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PropertTypes",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Properties",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PropertGroupTypes",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PrivacyTypes",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Languages",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Hosts",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Genders",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "CancellationPolicies",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AppUserLanguages",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AmenityTypes",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Amenities",
                newName: "IsDisplayed");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AirCovers",
                newName: "IsDisplayed");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisplayed",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisplayed",
                table: "GuestReviews",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisplayed",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsDisplayed",
                table: "GuestReviews");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "PropertyReviews",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "PropertyImages",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "PropertyGroups",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "PropertyAmenities",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "PropertTypes",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "Properties",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "PropertGroupTypes",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "PrivacyTypes",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "Languages",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "Hosts",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "Genders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "CancellationPolicies",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "AppUserLanguages",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "AmenityTypes",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "Amenities",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IsDisplayed",
                table: "AirCovers",
                newName: "Status");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "GuestReviews",
                type: "bit",
                nullable: true);
        }
    }
}
