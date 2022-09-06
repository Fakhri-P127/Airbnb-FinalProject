using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Persistance.Context.Migrations
{
    public partial class UpdatedSomeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "GuestReviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_GuestReviews_ReservationId",
                table: "GuestReviews",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestReviews_Reservations_ReservationId",
                table: "GuestReviews",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestReviews_Reservations_ReservationId",
                table: "GuestReviews");

            migrationBuilder.DropIndex(
                name: "IX_GuestReviews_ReservationId",
                table: "GuestReviews");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "GuestReviews");
        }
    }
}
