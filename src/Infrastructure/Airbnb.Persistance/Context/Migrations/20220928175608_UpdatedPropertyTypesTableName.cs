using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class UpdatedPropertyTypesTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertTypes_PropertyTypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertTypes_PropertyGroups_PropertyGroupId",
                table: "PropertTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertTypes",
                table: "PropertTypes");

            migrationBuilder.RenameTable(
                name: "PropertTypes",
                newName: "PropertyTypes");

            migrationBuilder.RenameIndex(
                name: "IX_PropertTypes_PropertyGroupId",
                table: "PropertyTypes",
                newName: "IX_PropertyTypes_PropertyGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertTypes_Name",
                table: "PropertyTypes",
                newName: "IX_PropertyTypes_Name");

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyGroupId",
                table: "PropertyTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyTypes",
                table: "PropertyTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyTypes_PropertyGroups_PropertyGroupId",
                table: "PropertyTypes",
                column: "PropertyGroupId",
                principalTable: "PropertyGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyTypes_PropertyGroups_PropertyGroupId",
                table: "PropertyTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyTypes",
                table: "PropertyTypes");

            migrationBuilder.RenameTable(
                name: "PropertyTypes",
                newName: "PropertTypes");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyTypes_PropertyGroupId",
                table: "PropertTypes",
                newName: "IX_PropertTypes_PropertyGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyTypes_Name",
                table: "PropertTypes",
                newName: "IX_PropertTypes_Name");

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyGroupId",
                table: "PropertTypes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertTypes",
                table: "PropertTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertTypes_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId",
                principalTable: "PropertTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertTypes_PropertyGroups_PropertyGroupId",
                table: "PropertTypes",
                column: "PropertyGroupId",
                principalTable: "PropertyGroups",
                principalColumn: "Id");
        }
    }
}
