using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.MSSQL.Migrations
{
    public partial class GameTheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Game",
                nullable: false,
                defaultValue: "Default");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Game");
        }
    }
}
