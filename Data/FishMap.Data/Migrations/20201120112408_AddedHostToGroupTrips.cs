using Microsoft.EntityFrameworkCore.Migrations;

namespace FishMap.Data.Migrations
{
    public partial class AddedHostToGroupTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupTrips_AspNetUsers_UserID",
                table: "UserGroupTrips");

            migrationBuilder.DropIndex(
                name: "IX_UserGroupTrips_UserID",
                table: "UserGroupTrips");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "UserGroupTrips");

            migrationBuilder.AddColumn<string>(
                name: "HostId",
                table: "UserGroupTrips",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "UserID",
                table: "UserGroupTrips",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupTrips_UserID",
                table: "UserGroupTrips",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupTrips_AspNetUsers_UserID",
                table: "UserGroupTrips",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
