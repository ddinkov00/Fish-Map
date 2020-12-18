using Microsoft.EntityFrameworkCore.Migrations;

namespace FishMap.Data.Migrations
{
    public partial class CommentsAddedToGroupTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GroupTripId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GroupTripId",
                table: "Comments",
                column: "GroupTripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_GroupTrips_GroupTripId",
                table: "Comments",
                column: "GroupTripId",
                principalTable: "GroupTrips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_GroupTrips_GroupTripId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_GroupTripId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "GroupTripId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
