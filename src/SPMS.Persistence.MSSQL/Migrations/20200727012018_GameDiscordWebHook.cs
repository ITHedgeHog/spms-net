using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.MSSQL.Migrations
{
    public partial class GameDiscordWebHook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscordWebHook",
                table: "Game",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscordWebHook",
                table: "Game");
        }
    }
}
