using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.MSSQL.Migrations
{
    public partial class ExtraGameData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Game",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "Game",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RobotsText",
                table: "Game",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "RobotsText",
                table: "Game");
        }
    }
}
