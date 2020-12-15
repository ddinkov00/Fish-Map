using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FishMap.Data.Migrations
{
    public partial class UserGropTripMadeDeletableModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserGroupTrips",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "UserGroupTrips",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserGroupTrips",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserGroupTrips",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupTrips_IsDeleted",
                table: "UserGroupTrips",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserGroupTrips_IsDeleted",
                table: "UserGroupTrips");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserGroupTrips");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "UserGroupTrips");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserGroupTrips");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserGroupTrips");
        }
    }
}
