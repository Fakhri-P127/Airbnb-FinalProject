using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class UpdatedValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HostId",
                table: "PropertyReviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyReviews_HostId",
                table: "PropertyReviews",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyReviews_Hosts_HostId",
                table: "PropertyReviews",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyReviews_Hosts_HostId",
                table: "PropertyReviews");

            migrationBuilder.DropIndex(
                name: "IX_PropertyReviews_HostId",
                table: "PropertyReviews");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "PropertyReviews");
        }
    }
}
