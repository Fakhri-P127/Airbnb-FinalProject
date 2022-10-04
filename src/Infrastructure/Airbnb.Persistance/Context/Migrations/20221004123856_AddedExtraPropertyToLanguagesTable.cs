using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class AddedExtraPropertyToLanguagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PropertyTypes_Name",
                table: "PropertyTypes");

            migrationBuilder.DropIndex(
                name: "IX_PropertyGroups_Name",
                table: "PropertyGroups");

            migrationBuilder.DropIndex(
                name: "IX_PrivacyTypes_Name",
                table: "PrivacyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Languages_Name",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Genders_Name",
                table: "Genders");

            migrationBuilder.DeleteData(
                table: "AppUserLanguages",
                keyColumn: "Id",
                keyValue: new Guid("20090680-1876-4bec-9d5d-ea8e17a43cb5"));

            migrationBuilder.DeleteData(
                table: "AppUserLanguages",
                keyColumn: "Id",
                keyValue: new Guid("71787a79-2e2b-47c3-b0b3-4576b38b78d0"));

            migrationBuilder.DeleteData(
                table: "AppUserLanguages",
                keyColumn: "Id",
                keyValue: new Guid("80392dfc-c4c8-409f-a88a-545f55399e2c"));

            migrationBuilder.DeleteData(
                table: "AppUserLanguages",
                keyColumn: "Id",
                keyValue: new Guid("89099bd7-cd47-4cfd-bc30-f2a0097ae921"));

            migrationBuilder.DeleteData(
                table: "AppUserLanguages",
                keyColumn: "Id",
                keyValue: new Guid("f3d622ae-4c62-4514-a962-d713828b3fb5"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a3214b8b-4250-4112-b460-6dd4f4c3b469"));

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: new Guid("3b61c0e6-3dc1-4b93-bf94-f611c963981e"));

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: new Guid("9c67d510-ce5c-431f-a0fc-0b484b7d677e"));

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: new Guid("cdb3b42e-1a68-4132-aed8-53027b085827"));

            migrationBuilder.DeleteData(
                table: "PrivacyTypes",
                keyColumn: "Id",
                keyValue: new Guid("808a7deb-a315-4e10-8452-559dcb535134"));

            migrationBuilder.DeleteData(
                table: "PrivacyTypes",
                keyColumn: "Id",
                keyValue: new Guid("c181ea90-47be-4652-a657-66ba2894b667"));

            migrationBuilder.DeleteData(
                table: "PrivacyTypes",
                keyColumn: "Id",
                keyValue: new Guid("f1a4b55b-4c6e-49af-8752-0522d6d1ad2f"));

            migrationBuilder.DeleteData(
                table: "PropertyGroups",
                keyColumn: "Id",
                keyValue: new Guid("ece14410-0615-4ba8-b192-fd4cc7a29148"));

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: new Guid("1b56bf3a-8ee5-468f-be33-3beb1e01713e"));

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: new Guid("f29126e0-157e-42f1-bb33-6a1177d8b23d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("066ecfc5-af54-41f4-82e4-5239d9c4109c"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("26eba03e-5f06-49a1-9b83-bea1ec1e4d76"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("6b4d5ca6-d36a-4392-82fa-cdf349e9273c"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("9e83464f-5b90-47f7-bf7e-674413c26c5c"));

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("e5505dc9-69a2-4d83-a062-6581810a3d17"));

            migrationBuilder.DeleteData(
                table: "PropertyGroups",
                keyColumn: "Id",
                keyValue: new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "PropertyTypes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "PropertyTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PropertyTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "PropertyGroups",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "PropertyGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PrivacyTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "PrivacyTypes",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "Languages",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "Genders",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Work",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "ProfilPicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "AppUserLanguages",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "Languages");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyTypes",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "PropertyTypes",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Icon",
                table: "PropertyTypes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PropertyTypes",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyGroups",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "PropertyGroups",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "PropertyGroups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PrivacyTypes",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "PrivacyTypes",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "Languages",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "Genders",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Work",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfilPicture",
                table: "AspNetUsers",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lastname",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDisplayed",
                table: "AppUserLanguages",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "About", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "Firstname", "GenderId", "IsDisplayed", "Lastname", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilPicture", "SecurityStamp", "TwoFactorEnabled", "UserName", "Work" },
                values: new object[,]
                {
                    { new Guid("a3214b8b-4250-4112-b460-6dd4f4c3b469"), null, 0, "b36301df-3a39-45cd-95a3-38e824523303", new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(4147), new DateTime(1999, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "seedEmail2@gmail.com", true, "Eli", null, true, "Efendiyev", false, null, new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(4150), null, null, null, "+994503660012", true, null, null, false, "eli1999", null },
                    { new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), null, 0, "ccdb459a-a92c-4ec9-bae3-96421b4380e6", new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(4067), new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "seedEmail1@gmail.com", true, "Fexri", null, true, "Efendiyev", false, null, new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(4079), null, null, null, "+994503661012", true, null, null, false, "fexri2000", null }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "CreatedAt", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("3b61c0e6-3dc1-4b93-bf94-f611c963981e"), new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1464), true, new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1465), "Female" },
                    { new Guid("9c67d510-ce5c-431f-a0fc-0b484b7d677e"), new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1469), true, new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1470), "Other" },
                    { new Guid("cdb3b42e-1a68-4132-aed8-53027b085827"), new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1441), true, new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1449), "Male" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreatedAt", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("066ecfc5-af54-41f4-82e4-5239d9c4109c"), new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(564), true, new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(583), "Turkish" },
                    { new Guid("26eba03e-5f06-49a1-9b83-bea1ec1e4d76"), new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(542), true, new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(544), "Japanese" },
                    { new Guid("6b4d5ca6-d36a-4392-82fa-cdf349e9273c"), new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(552), true, new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(555), "Russian" },
                    { new Guid("9e83464f-5b90-47f7-bf7e-674413c26c5c"), new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(532), true, new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(534), "English" },
                    { new Guid("e5505dc9-69a2-4d83-a062-6581810a3d17"), new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(519), true, new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(523), "Azerbaijani" }
                });

            migrationBuilder.InsertData(
                table: "PrivacyTypes",
                columns: new[] { "Id", "CreatedAt", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("808a7deb-a315-4e10-8452-559dcb535134"), new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(766), true, new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(768), "Shared house" },
                    { new Guid("c181ea90-47be-4652-a657-66ba2894b667"), new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(776), true, new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(777), "Private room" },
                    { new Guid("f1a4b55b-4c6e-49af-8752-0522d6d1ad2f"), new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(743), true, new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(752), "Full apartment" }
                });

            migrationBuilder.InsertData(
                table: "PropertyGroups",
                columns: new[] { "Id", "CreatedAt", "Image", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c"), new DateTime(2022, 10, 3, 14, 32, 14, 771, DateTimeKind.Local).AddTicks(9732), "54874e8e-2699-4ff7-adab-875f528dee59.jpg", true, new DateTime(2022, 10, 3, 14, 32, 14, 771, DateTimeKind.Local).AddTicks(9744), "Apartment" },
                    { new Guid("ece14410-0615-4ba8-b192-fd4cc7a29148"), new DateTime(2022, 10, 3, 14, 32, 14, 771, DateTimeKind.Local).AddTicks(9785), "520f85dc-c9a8-45c6-b2fc-179150d10285.jpg", true, new DateTime(2022, 10, 3, 14, 32, 14, 771, DateTimeKind.Local).AddTicks(9786), "House" }
                });

            migrationBuilder.InsertData(
                table: "AppUserLanguages",
                columns: new[] { "Id", "AppUserId", "CreatedAt", "IsDisplayed", "LanguageId", "ModifiedAt" },
                values: new object[,]
                {
                    { new Guid("20090680-1876-4bec-9d5d-ea8e17a43cb5"), new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(7360), true, new Guid("6b4d5ca6-d36a-4392-82fa-cdf349e9273c"), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(7361) },
                    { new Guid("71787a79-2e2b-47c3-b0b3-4576b38b78d0"), new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(7333), true, new Guid("066ecfc5-af54-41f4-82e4-5239d9c4109c"), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(7335) },
                    { new Guid("80392dfc-c4c8-409f-a88a-545f55399e2c"), new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(7311), true, new Guid("26eba03e-5f06-49a1-9b83-bea1ec1e4d76"), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(7313) },
                    { new Guid("89099bd7-cd47-4cfd-bc30-f2a0097ae921"), new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(7286), true, new Guid("e5505dc9-69a2-4d83-a062-6581810a3d17"), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(7292) },
                    { new Guid("f3d622ae-4c62-4514-a962-d713828b3fb5"), new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(7324), true, new Guid("9e83464f-5b90-47f7-bf7e-674413c26c5c"), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(7325) }
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Icon", "IsDisplayed", "ModifiedAt", "Name", "PropertyGroupId" },
                values: new object[,]
                {
                    { new Guid("1b56bf3a-8ee5-468f-be33-3beb1e01713e"), new DateTime(2022, 10, 3, 14, 32, 14, 773, DateTimeKind.Local).AddTicks(915), "A furnished rental property that includes a kitchen and bathroom and may offer some guest services, like a reception desk.", "<i class='fa-solid fa-apartment'></i>", true, new DateTime(2022, 10, 3, 14, 32, 14, 773, DateTimeKind.Local).AddTicks(919), "Vacation House", new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c") },
                    { new Guid("f29126e0-157e-42f1-bb33-6a1177d8b23d"), new DateTime(2022, 10, 3, 14, 32, 14, 773, DateTimeKind.Local).AddTicks(883), "A place within a multi-unit building or complex owned by the residents.", "<i class=\"fa-solid fa-apartment\"></i>", true, new DateTime(2022, 10, 3, 14, 32, 14, 773, DateTimeKind.Local).AddTicks(891), "Condo", new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTypes_Name",
                table: "PropertyTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyGroups_Name",
                table: "PropertyGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrivacyTypes_Name",
                table: "PrivacyTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Name",
                table: "Languages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Name",
                table: "Genders",
                column: "Name",
                unique: true);
        }
    }
}
