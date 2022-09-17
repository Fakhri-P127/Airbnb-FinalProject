using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class RemovedPropertyAndPropertyReviewRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestReviews_AspNetUsers_AppUserId",
                table: "GuestReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyReviews_Properties_PropertyId",
                table: "PropertyReviews");

            migrationBuilder.DropIndex(
                name: "IX_PropertyReviews_PropertyId",
                table: "PropertyReviews");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "PropertyReviews");

            migrationBuilder.AlterColumn<Guid>(
                name: "HostId",
                table: "PropertyReviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "GuestReviews",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "GuestReviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestReviews_AspNetUsers_AppUserId",
                table: "GuestReviews",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestReviews_AspNetUsers_AppUserId",
                table: "GuestReviews");

            migrationBuilder.AlterColumn<Guid>(
                name: "HostId",
                table: "PropertyReviews",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "PropertyReviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "GuestReviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(350)",
                oldMaxLength: 350);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "GuestReviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyReviews_PropertyId",
                table: "PropertyReviews",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestReviews_AspNetUsers_AppUserId",
                table: "GuestReviews",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyReviews_Properties_PropertyId",
                table: "PropertyReviews",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
