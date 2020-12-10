using Microsoft.EntityFrameworkCore.Migrations;

namespace FishMap.Data.Migrations
{
    public partial class DescriptionAddedToGroupTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "GroupTrips",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "GroupTrips");
        }
    }
}
