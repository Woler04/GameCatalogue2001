using Microsoft.EntityFrameworkCore.Migrations;

namespace GameCatalogue.Migrations
{
    public partial class GameBetaImageFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imageUrl",
                table: "GamesModel",
                newName: "ImageUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "GamesModel",
                newName: "imageUrl");
        }
    }
}
