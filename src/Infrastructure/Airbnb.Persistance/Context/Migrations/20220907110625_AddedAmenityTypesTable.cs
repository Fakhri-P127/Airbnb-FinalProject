using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class AddedAmenityTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmenityType",
                table: "Amenities");

            migrationBuilder.AddColumn<Guid>(
                name: "AmenityTypeId",
                table: "Amenities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AmenityTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_AmenityTypeId",
                table: "Amenities",
                column: "AmenityTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_AmenityTypes_AmenityTypeId",
                table: "Amenities",
                column: "AmenityTypeId",
                principalTable: "AmenityTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_AmenityTypes_AmenityTypeId",
                table: "Amenities");

            migrationBuilder.DropTable(
                name: "AmenityTypes");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_AmenityTypeId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "AmenityTypeId",
                table: "Amenities");

            migrationBuilder.AddColumn<string>(
                name: "AmenityType",
                table: "Amenities",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }
    }
}
