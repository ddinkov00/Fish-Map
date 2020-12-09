using Microsoft.EntityFrameworkCore.Migrations;

namespace FishMap.Data.Migrations
{
    public partial class ImageModelRenameUriToUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uri",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Uri",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
