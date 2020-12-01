using Microsoft.EntityFrameworkCore.Migrations;

namespace FishMap.Data.Migrations
{
    public partial class RelationImageToTripAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fish_FishSpecies_FishKindId",
                table: "Fish");

            migrationBuilder.DropIndex(
                name: "IX_Fish_FishKindId",
                table: "Fish");

            migrationBuilder.DropColumn(
                name: "FishKindId",
                table: "Fish");

            migrationBuilder.AddColumn<int>(
                name: "FishId",
                table: "Images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FishSpeciesId",
                table: "Fish",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_FishId",
                table: "Images",
                column: "FishId");

            migrationBuilder.CreateIndex(
                name: "IX_Fish_FishSpeciesId",
                table: "Fish",
                column: "FishSpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fish_FishSpecies_FishSpeciesId",
                table: "Fish",
                column: "FishSpeciesId",
                principalTable: "FishSpecies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Fish_FishId",
                table: "Images",
                column: "FishId",
                principalTable: "Fish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fish_FishSpecies_FishSpeciesId",
                table: "Fish");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Fish_FishId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_FishId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Fish_FishSpeciesId",
                table: "Fish");

            migrationBuilder.DropColumn(
                name: "FishId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "FishSpeciesId",
                table: "Fish");

            migrationBuilder.AddColumn<int>(
                name: "FishKindId",
                table: "Fish",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fish_FishKindId",
                table: "Fish",
                column: "FishKindId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fish_FishSpecies_FishKindId",
                table: "Fish",
                column: "FishKindId",
                principalTable: "FishSpecies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
