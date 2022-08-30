using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class AddedPropertiesTableAndRelatedTablesWithIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "GenderId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AirCovers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BookingProtectionGuarantee = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CheckInGuarantee = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    GetWhatYouBookedGuarantee = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FullDaySafetyLine = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FindMore = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirCovers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmenityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CancellationPolicies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FullRefund = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PartialRefund = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NoRefund = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancellationPolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
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
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
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
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivacyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivacyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserLanguages_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppUserLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertGroupTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    City = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinNightCount = table.Column<byte>(type: "tinyint", nullable: false),
                    MaxNightCount = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)60),
                    MaxGuestCount = table.Column<byte>(type: "tinyint", nullable: false),
                    AdultCount = table.Column<byte>(type: "tinyint", nullable: false),
                    ChildrenCount = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    BathroomCount = table.Column<byte>(type: "tinyint", nullable: false),
                    BedroomCount = table.Column<byte>(type: "tinyint", nullable: false),
                    BedCount = table.Column<byte>(type: "tinyint", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,8)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: false),
                    PropertyGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrivacyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AirCoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CancellationPolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_AirCovers_AirCoverId",
                        column: x => x.AirCoverId,
                        principalTable: "AirCovers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_CancellationPolicies_CancellationPolicyId",
                        column: x => x.CancellationPolicyId,
                        principalTable: "CancellationPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_PrivacyTypes_PrivacyTypeId",
                        column: x => x.PrivacyTypeId,
                        principalTable: "PrivacyTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Properties_PropertTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyGroups_PropertyGroupId",
                        column: x => x.PropertyGroupId,
                        principalTable: "PropertyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAmenities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmenityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAmenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyAmenities_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyAmenities_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alternative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_Description",
                table: "Amenities",
                column: "Description",
                unique: true,
                filter: "[Description] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_Icon",
                table: "Amenities",
                column: "Icon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_Name",
                table: "Amenities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLanguages_AppUserId",
                table: "AppUserLanguages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLanguages_LanguageId",
                table: "AppUserLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CancellationPolicies_Name",
                table: "CancellationPolicies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrivacyTypes_Name",
                table: "PrivacyTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertGroupTypes_PropertyGroupId",
                table: "PropertGroupTypes",
                column: "PropertyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertGroupTypes_PropertyTypeId",
                table: "PropertGroupTypes",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AirCoverId",
                table: "Properties",
                column: "AirCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AppUserId",
                table: "Properties",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CancellationPolicyId",
                table: "Properties",
                column: "CancellationPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PrivacyTypeId",
                table: "Properties",
                column: "PrivacyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyGroupId",
                table: "Properties",
                column: "PropertyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAmenities_AmenityId",
                table: "PropertyAmenities",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAmenities_PropertyId",
                table: "PropertyAmenities",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyId",
                table: "PropertyImages",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Genders_GenderId",
                table: "AspNetUsers",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Genders_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AppUserLanguages");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "PropertGroupTypes");

            migrationBuilder.DropTable(
                name: "PropertyAmenities");

            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "AirCovers");

            migrationBuilder.DropTable(
                name: "CancellationPolicies");

            migrationBuilder.DropTable(
                name: "PrivacyTypes");

            migrationBuilder.DropTable(
                name: "PropertTypes");

            migrationBuilder.DropTable(
                name: "PropertyGroups");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }
    }
}
