using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class UpdatedRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hosts_HostId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HostId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Hosts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hosts_AppUserId",
                table: "Hosts",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Hosts_AspNetUsers_AppUserId",
                table: "Hosts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hosts_AspNetUsers_AppUserId",
                table: "Hosts");

            migrationBuilder.DropIndex(
                name: "IX_Hosts_AppUserId",
                table: "Hosts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Hosts");

            migrationBuilder.AddColumn<Guid>(
                name: "HostId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HostId",
                table: "AspNetUsers",
                column: "HostId",
                unique: true,
                filter: "[HostId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hosts_HostId",
                table: "AspNetUsers",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id");
        }
    }
}
