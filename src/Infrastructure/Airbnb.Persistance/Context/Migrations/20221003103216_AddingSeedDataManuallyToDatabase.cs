using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class AddingSeedDataManuallyToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "ModifiedAt" },
                values: new object[] { "ccdb459a-a92c-4ec9-bae3-96421b4380e6", new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(4067), new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(4079) });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "About", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "Firstname", "GenderId", "IsDisplayed", "Lastname", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilPicture", "SecurityStamp", "TwoFactorEnabled", "UserName", "Work" },
                values: new object[] { new Guid("a3214b8b-4250-4112-b460-6dd4f4c3b469"), null, 0, "b36301df-3a39-45cd-95a3-38e824523303", new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(4147), new DateTime(1999, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "seedEmail2@gmail.com", true, "Eli", null, true, "Efendiyev", false, null, new DateTime(2022, 10, 3, 14, 32, 14, 766, DateTimeKind.Local).AddTicks(4150), null, null, null, "+994503660012", true, null, null, false, "eli1999", null });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "CreatedAt", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("3b61c0e6-3dc1-4b93-bf94-f611c963981e"), new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1464), true, new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1465), "Female" },
                    { new Guid("9c67d510-ce5c-431f-a0fc-0b484b7d677e"), new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1469), true, new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1470), "Other" },
                    { new Guid("cdb3b42e-1a68-4132-aed8-53027b085827"), new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1441), true, new DateTime(2022, 10, 3, 14, 32, 14, 767, DateTimeKind.Local).AddTicks(1449), "Male" }
                });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("066ecfc5-af54-41f4-82e4-5239d9c4109c"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(564), new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(583) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("26eba03e-5f06-49a1-9b83-bea1ec1e4d76"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(542), new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(544) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("6b4d5ca6-d36a-4392-82fa-cdf349e9273c"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(552), new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(555) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("9e83464f-5b90-47f7-bf7e-674413c26c5c"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(532), new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(534) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("e5505dc9-69a2-4d83-a062-6581810a3d17"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(519), new DateTime(2022, 10, 3, 14, 32, 14, 768, DateTimeKind.Local).AddTicks(523) });

            migrationBuilder.InsertData(
                table: "PrivacyTypes",
                columns: new[] { "Id", "CreatedAt", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("808a7deb-a315-4e10-8452-559dcb535134"), new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(766), true, new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(768), "Shared house" },
                    { new Guid("c181ea90-47be-4652-a657-66ba2894b667"), new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(776), true, new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(777), "Private room" },
                    { new Guid("f1a4b55b-4c6e-49af-8752-0522d6d1ad2f"), new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(743), true, new DateTime(2022, 10, 3, 14, 32, 14, 770, DateTimeKind.Local).AddTicks(752), "Full apartment" }
                });

            migrationBuilder.UpdateData(
                table: "PropertyGroups",
                keyColumn: "Id",
                keyValue: new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 32, 14, 771, DateTimeKind.Local).AddTicks(9732), new DateTime(2022, 10, 3, 14, 32, 14, 771, DateTimeKind.Local).AddTicks(9744) });

            migrationBuilder.InsertData(
                table: "PropertyGroups",
                columns: new[] { "Id", "CreatedAt", "Image", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[] { new Guid("ece14410-0615-4ba8-b192-fd4cc7a29148"), new DateTime(2022, 10, 3, 14, 32, 14, 771, DateTimeKind.Local).AddTicks(9785), "520f85dc-c9a8-45c6-b2fc-179150d10285.jpg", true, new DateTime(2022, 10, 3, 14, 32, 14, 771, DateTimeKind.Local).AddTicks(9786), "House" });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Icon", "IsDisplayed", "ModifiedAt", "Name", "PropertyGroupId" },
                values: new object[,]
                {
                    { new Guid("1b56bf3a-8ee5-468f-be33-3beb1e01713e"), new DateTime(2022, 10, 3, 14, 32, 14, 773, DateTimeKind.Local).AddTicks(915), "A furnished rental property that includes a kitchen and bathroom and may offer some guest services, like a reception desk.", "<i class='fa-solid fa-apartment'></i>", true, new DateTime(2022, 10, 3, 14, 32, 14, 773, DateTimeKind.Local).AddTicks(919), "Vacation House", new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c") },
                    { new Guid("f29126e0-157e-42f1-bb33-6a1177d8b23d"), new DateTime(2022, 10, 3, 14, 32, 14, 773, DateTimeKind.Local).AddTicks(883), "A place within a multi-unit building or complex owned by the residents.", "<i class=\"fa-solid fa-apartment\"></i>", true, new DateTime(2022, 10, 3, 14, 32, 14, 773, DateTimeKind.Local).AddTicks(891), "Condo", new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7375a39-5d8b-4a87-be3e-f337b17351f8"),
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "ModifiedAt" },
                values: new object[] { "eab246b0-6759-4798-8c08-72a110745308", new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(5736), new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(5750) });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "About", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "DateOfBirth", "Email", "EmailConfirmed", "Firstname", "GenderId", "IsDisplayed", "Lastname", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilPicture", "SecurityStamp", "TwoFactorEnabled", "UserName", "Work" },
                values: new object[] { new Guid("f0f3127e-ef63-4bc7-a9b8-437dfc1531db"), null, 0, "2ef5e917-5e58-4cb5-8959-d2fadc647693", new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(5817), new DateTime(1999, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "seedEmail2@gmail.com", true, "Eli", null, true, "Efendiyev", false, null, new DateTime(2022, 10, 3, 14, 24, 44, 732, DateTimeKind.Local).AddTicks(5821), null, null, null, "+994503660012", true, null, null, false, "eli1999", null });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "CreatedAt", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("02afc99a-7a4c-4b73-9fa9-1e1dc64c7a68"), new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3796), true, new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3803), "Male" },
                    { new Guid("d825d42b-563d-4063-a641-42ba7d6fc8e7"), new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3841), true, new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3844), "Other" },
                    { new Guid("f0057362-686f-4629-8944-b679c01f9f80"), new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3828), true, new DateTime(2022, 10, 3, 14, 24, 44, 733, DateTimeKind.Local).AddTicks(3831), "Female" }
                });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("066ecfc5-af54-41f4-82e4-5239d9c4109c"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8541), new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8568) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("26eba03e-5f06-49a1-9b83-bea1ec1e4d76"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8519), new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8522) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("6b4d5ca6-d36a-4392-82fa-cdf349e9273c"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8530), new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8533) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("9e83464f-5b90-47f7-bf7e-674413c26c5c"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8510), new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8510) });

            migrationBuilder.UpdateData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("e5505dc9-69a2-4d83-a062-6581810a3d17"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8484), new DateTime(2022, 10, 3, 14, 24, 44, 744, DateTimeKind.Local).AddTicks(8503) });

            migrationBuilder.InsertData(
                table: "PrivacyTypes",
                columns: new[] { "Id", "CreatedAt", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0dc3838c-5912-48fa-89c9-83cc8d0aee5b"), new DateTime(2022, 10, 3, 14, 24, 44, 747, DateTimeKind.Local).AddTicks(2579), true, new DateTime(2022, 10, 3, 14, 24, 44, 747, DateTimeKind.Local).AddTicks(2580), "Private room" },
                    { new Guid("69843833-f5d2-49dd-9516-19300e14f408"), new DateTime(2022, 10, 3, 14, 24, 44, 747, DateTimeKind.Local).AddTicks(2547), true, new DateTime(2022, 10, 3, 14, 24, 44, 747, DateTimeKind.Local).AddTicks(2564), "Shared house" }
                });

            migrationBuilder.UpdateData(
                table: "PropertyGroups",
                keyColumn: "Id",
                keyValue: new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2022, 10, 3, 14, 24, 44, 749, DateTimeKind.Local).AddTicks(3986), new DateTime(2022, 10, 3, 14, 24, 44, 749, DateTimeKind.Local).AddTicks(4003) });

            migrationBuilder.InsertData(
                table: "PropertyGroups",
                columns: new[] { "Id", "CreatedAt", "Image", "IsDisplayed", "ModifiedAt", "Name" },
                values: new object[] { new Guid("0b17c1b1-c334-43b2-b946-13e1982cb4ff"), new DateTime(2022, 10, 3, 14, 24, 44, 749, DateTimeKind.Local).AddTicks(4049), "520f85dc-c9a8-45c6-b2fc-179150d10285.jpg", true, new DateTime(2022, 10, 3, 14, 24, 44, 749, DateTimeKind.Local).AddTicks(4052), "House" });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Icon", "IsDisplayed", "ModifiedAt", "Name", "PropertyGroupId" },
                values: new object[,]
                {
                    { new Guid("3455dc1f-79d3-43c4-a873-612a5066cbbc"), new DateTime(2022, 10, 3, 14, 24, 44, 750, DateTimeKind.Local).AddTicks(7165), "A place within a multi-unit building or complex owned by the residents.", "<i class=\"fa-solid fa-apartment\"></i>", true, new DateTime(2022, 10, 3, 14, 24, 44, 750, DateTimeKind.Local).AddTicks(7181), "Condo", new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c") },
                    { new Guid("80fabdcc-8f7a-4e95-8684-82bde34e2f5d"), new DateTime(2022, 10, 3, 14, 24, 44, 750, DateTimeKind.Local).AddTicks(7219), "A furnished rental property that includes a kitchen and bathroom and may offer some guest services, like a reception desk.", "<i class='fa-solid fa-apartment'></i>", true, new DateTime(2022, 10, 3, 14, 24, 44, 750, DateTimeKind.Local).AddTicks(7222), "Vacation House", new Guid("9138421c-0e9f-4d23-85dd-f2fdf3a4854c") }
                });
        }
    }
}
