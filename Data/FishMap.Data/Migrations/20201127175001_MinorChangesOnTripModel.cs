using Microsoft.EntityFrameworkCore.Migrations;

namespace FishMap.Data.Migrations
{
    public partial class MinorChangesOnTripModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_FishSpecies_FishSpeciesId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_FishSpeciesId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "FishSpeciesId",
                table: "Trips");

            migrationBuilder.AddColumn<string>(
                name: "WaterPoolName",
                table: "Trips",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WaterPoolName",
                table: "GroupTrips",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WaterPoolName",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "WaterPoolName",
                table: "GroupTrips");

            migrationBuilder.AddColumn<int>(
                name: "FishSpeciesId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_FishSpeciesId",
                table: "Trips",
                column: "FishSpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_FishSpecies_FishSpeciesId",
                table: "Trips",
                column: "FishSpeciesId",
                principalTable: "FishSpecies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
