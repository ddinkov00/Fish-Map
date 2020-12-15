using Microsoft.EntityFrameworkCore.Migrations;

namespace FishMap.Data.Migrations
{
    public partial class UserGroupTripsMinorChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupTrips_AspNetUsers_HostId",
                table: "UserGroupTrips");

            migrationBuilder.DropIndex(
                name: "IX_UserGroupTrips_HostId",
                table: "UserGroupTrips");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "UserGroupTrips");

            migrationBuilder.AddColumn<string>(
                name: "GuestId",
                table: "UserGroupTrips",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupTrips_GuestId",
                table: "UserGroupTrips",
                column: "GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupTrips_AspNetUsers_GuestId",
                table: "UserGroupTrips",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupTrips_AspNetUsers_GuestId",
                table: "UserGroupTrips");

            migrationBuilder.DropIndex(
                name: "IX_UserGroupTrips_GuestId",
                table: "UserGroupTrips");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "UserGroupTrips");

            migrationBuilder.AddColumn<string>(
                name: "HostId",
                table: "UserGroupTrips",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupTrips_HostId",
                table: "UserGroupTrips",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupTrips_AspNetUsers_HostId",
                table: "UserGroupTrips",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
