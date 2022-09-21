using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class AddedStatesTableAndItsRelatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_AirCovers_AirCoverId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PrivacyTypes_PrivacyTypeId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Properties");

            migrationBuilder.AlterColumn<Guid>(
                name: "PrivacyTypeId",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "MinNightCount",
                table: "Properties",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AirCoverId",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "IsPetAllowed",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDisplayed = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDisplayed = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDisplayed = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDisplayed = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_State_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_State_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_StateId",
                table: "Properties",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_RegionId",
                table: "Country",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CityId",
                table: "State",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_State_RegionId",
                table: "State",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_AirCovers_AirCoverId",
                table: "Properties",
                column: "AirCoverId",
                principalTable: "AirCovers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PrivacyTypes_PrivacyTypeId",
                table: "Properties",
                column: "PrivacyTypeId",
                principalTable: "PrivacyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_State_StateId",
                table: "Properties",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_AirCovers_AirCoverId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PrivacyTypes_PrivacyTypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_State_StateId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropIndex(
                name: "IX_Properties_StateId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsPetAllowed",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Properties");

            migrationBuilder.AlterColumn<Guid>(
                name: "PrivacyTypeId",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<byte>(
                name: "MinNightCount",
                table: "Properties",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "Properties",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AirCoverId",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Properties",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Properties",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Properties",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_AirCovers_AirCoverId",
                table: "Properties",
                column: "AirCoverId",
                principalTable: "AirCovers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PrivacyTypes_PrivacyTypeId",
                table: "Properties",
                column: "PrivacyTypeId",
                principalTable: "PrivacyTypes",
                principalColumn: "Id");
        }
    }
}
