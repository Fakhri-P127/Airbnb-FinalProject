using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class salamdasdas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "About", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "Firstname", "GenderId", "IsDisplayed", "Lastname", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilPicture", "SecurityStamp", "TwoFactorEnabled", "UserName", "Work" },
                values: new object[,]
                {
                    { new Guid("f0f3127e-ef63-4bc7-a9b8-437dfc1531db"), null, 0, "2ef5e917-5e58-4cb5-8959-d2fadc647693", new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(5817), new DateTime(1999, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "seedEmail2@gmail.com", true, "Eli", null, true, "Efendiyev", false, null, new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(5821), null, null, null, "+994503660012", true, null, null, false, "eli1999", null },
                    { new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), null, 0, "eab246b0-6759-4798-8c08-72a110745308", new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(5736), new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "seedEmail1@gmail.com", true, "Fexri", null, true, "Efendiyev", false, null, new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(5750), null, null, null, "+994503661012", true, null, null, false, "fexri2000", null }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "CreatedAt", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("02afc99a-7a4c-4b73-9fa9-1e1dc64c7a68"), new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3796), true, new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3803), "Male" },
                    { new Guid("d825d42b-563d-4063-a641-42ba7d6fc8e7"), new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3841), true, new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3844), "Other" },
                    { new Guid("f0057362-686f-4629-8944-b679c01f9f80"), new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3828), true, new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3831), "Female" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CreatedAt", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("066ecfc5-af54-41f4-82e4-5239d9c4109c"), new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8541), true, new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8568), "Turkish" },
                    { new Guid("26eba03e-5f06-49a1-9b83-bea1ec1e4d76"), new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8519), true, new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8522), "Japanese" },
                    { new Guid("6b4d5ca6-d36a-4392-82fa-cdf349e9273c"), new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8530), true, new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8533), "Russian" },
                    { new Guid("9e83464f-5b90-47f7-bf7e-674413c26c5c"), new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8510), true, new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8510), "English" },
                    { new Guid("e5505dc9-69a2-4d83-a062-6581810a3d17"), new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8484), true, new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8503), "Azerbaijani" }
                });

            migrationBuilder.InsertData(
                table: "PrivacyTypes",
                columns: new[] { "Id", "CreatedAt", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0dc3838c-5912-48fa-89c9-83cc8d0aee5b"), new DateTime(2022, 10, 3, 14, 24, 44, 747, DateTimeKind.Local).AddTicks(2579), true, new DateTime(2022, 10, 3, 14, 24, 44, 747, DateTimeKind.Local).AddTicks(2580), "Private room" },
                    { new Guid("69843833-f5d2-49dd-9516-19300e14f408"), new DateTime(2022, 10, 3, 14, 24, 44, 747, DateTimeKind.Local).AddTicks(2547), true, new DateTime(2022, 10, 3, 14, 24, 44, 747, DateTimeKind.Local).AddTicks(2564), "Shared house" }
                });

            migrationBuilder.InsertData(
                table: "PropertyGroups",
                columns: new[] { "Id", "CreatedAt", "Image", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0b17c1b1-c334-43b2-b946-13e1982cb4ff"), new DateTime(2022, 10, 3, 14, 24, 44, 749, DateTimeKind.Local).AddTicks(4049), "520f85dc-c9a8-45c6-b2fc-179150d10285.jpg", true, new DateTime(2022, 10, 3, 14, 24, 44, 749, DateTimeKind.Local).AddTicks(4052), "House" },
                    { new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c"), new DateTime(2022, 10, 3, 14, 24, 44, 749, DateTimeKind.Local).AddTicks(3986), "54874e8e-2699-4ff7-adab-875f528dee59.jpg", true, new DateTime(2022, 10, 3, 14, 24, 44, 749, DateTimeKind.Local).AddTicks(4003), "Apartment" }
                });

            migrationBuilder.InsertData(
                table: "AppUserLanguages",
                columns: new[] { "Id", "AppUserId", "CreatedAt", "IsDisplayed", "LanguageId", "ModifiedAt" },
                values: new object[,]
                {
                    { new Guid("2ad46b25-f5b7-4a66-a1e8-41f863cbac74"), new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(9436), true, new Guid("9e83464f-5b90-47f7-bf7e-674413c26c5c"), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(9438) },
                    { new Guid("35493f72-bbbd-4610-a515-3c8d981c3a6e"), new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(9452), true, new Guid("066ecfc5-af54-41f4-82e4-5239d9c4109c"), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(9454) },
                    { new Guid("5fb1559b-f250-437d-bcb6-eb8400320009"), new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(9466), true, new Guid("6b4d5ca6-d36a-4392-82fa-cdf349e9273c"), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(9469) },
                    { new Guid("63e9ddf9-6be1-4b00-93bd-fff0c459b4fe"), new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(9421), true, new Guid("26eba03e-5f06-49a1-9b83-bea1ec1e4d76"), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(9423) },
                    { new Guid("a6af3a8c-26ea-4621-9a9b-54be4892f254"), new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(9401), true, new Guid("e5505dc9-69a2-4d83-a062-6581810a3d17"), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(9407) }
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Icon", "IsDisplayed", "ModifiedAt", "Name", "PropertyGroupId" },
                values: new object[,]
                {
                    { new Guid("3455dc1f-79d3-43c4-a873-612a5066cbbc"), new DateTime(2022, 10, 3, 14, 24, 44, 750, DateTimeKind.Local).AddTicks(7165), "A place within a multi-unit building or complex owned by the residents.", "<i class=\"fa-solid fa-apartment\"></i>", true, new DateTime(2022, 10, 3, 14, 24, 44, 750, DateTimeKind.Local).AddTicks(7181), "Condo", new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c") },
                    { new Guid("80fabdcc-8f7a-4e95-8684-82bde34e2f5d"), new DateTime(2022, 10, 3, 14, 24, 44, 750, DateTimeKind.Local).AddTicks(7219), "A furnished rental property that includes a kitchen and bathroom and may offer some guest services, like a reception desk.", "<i class='fa-solid fa-apartment'></i>", true, new DateTime(2022, 10, 3, 14, 24, 44, 750, DateTimeKind.Local).AddTicks(7222), "Vacation House", new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUserLanguages",
                keyColumn: "Id",
                keyValue: new Guid("2ad46b25-f5b7-4a66-a1e8-41f863cbac74"));

            migrationBuilder.DeleteData(
                table: "AppUserLanguages",
                keyColumn: "Id",
                keyValue: new Guid("35493f72-bbbd-4610-a515-3c8d981c3a6e"));

            migrationBuilder.DeleteData(
                table: "AppUserLanguages",
                keyColumn: "Id",
                keyValue: new Guid("5fb1559b-f250-437d-bcb6-eb8400320009"));

            migrationBuilder.DeleteData(
                table: "AppUserLanguages",
                keyColumn: "Id",
                keyValue: new Guid("63e9ddf9-6be1-4b00-93bd-fff0c459b4fe"));

            migrationBuilder.DeleteData(
                table: "AppUserLanguages",
                keyColumn: "Id",
                keyValue: new Guid("a6af3a8c-26ea-4621-9a9b-54be4892f254"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f0f3127e-ef63-4bc7-a9b8-437dfc1531db"));

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: new Guid("02afc99a-7a4c-4b73-9fa9-1e1dc64c7a68"));

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: new Guid("d825d42b-563d-4063-a641-42ba7d6fc8e7"));

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: new Guid("f0057362-686f-4629-8944-b679c01f9f80"));

            migrationBuilder.DeleteData(
                table: "PrivacyTypes",
                keyColumn: "Id",
                keyValue: new Guid("0dc3838c-5912-48fa-89c9-83cc8d0aee5b"));

            migrationBuilder.DeleteData(
                table: "PrivacyTypes",
                keyColumn: "Id",
                keyValue: new Guid("69843833-f5d2-49dd-9516-19300e14f408"));

            migrationBuilder.DeleteData(
                table: "PropertyGroups",
                keyColumn: "Id",
                keyValue: new Guid("0b17c1b1-c334-43b2-b946-13e1982cb4ff"));

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: new Guid("3455dc1f-79d3-43c4-a873-612a5066cbbc"));

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: new Guid("80fabdcc-8f7a-4e95-8684-82bde34e2f5d"));

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
        }
    }
}
