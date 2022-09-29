using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class UpdatedPropertyGroupAndTypeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertGroupTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyGroupId",
                table: "PropertTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertTypes_PropertyGroupId",
                table: "PropertTypes",
                column: "PropertyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertTypes_PropertyGroups_PropertyGroupId",
                table: "PropertTypes",
                column: "PropertyGroupId",
                principalTable: "PropertyGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertTypes_PropertyGroups_PropertyGroupId",
                table: "PropertTypes");

            migrationBuilder.DropIndex(
                name: "IX_PropertTypes_PropertyGroupId",
                table: "PropertTypes");

            migrationBuilder.DropColumn(
                name: "PropertyGroupId",
                table: "PropertTypes");

            migrationBuilder.CreateTable(
                name: "PropertGroupTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDisplayed = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertGroupTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertGroupTypes_PropertTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertGroupTypes_PropertyGroups_PropertyGroupId",
                        column: x => x.PropertyGroupId,
                        principalTable: "PropertyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertGroupTypes_PropertyGroupId",
                table: "PropertGroupTypes",
                column: "PropertyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertGroupTypes_PropertyTypeId",
                table: "PropertGroupTypes",
                column: "PropertyTypeId");
        }
    }
}
