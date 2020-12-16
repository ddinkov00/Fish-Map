namespace FishMap.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class TripToNearestTownRelationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NearestTownId",
                table: "Trips",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_NearestTownId",
                table: "Trips",
                column: "NearestTownId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Towns_NearestTownId",
                table: "Trips",
                column: "NearestTownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Towns_NearestTownId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_NearestTownId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "NearestTownId",
                table: "Trips");
        }
    }
}
