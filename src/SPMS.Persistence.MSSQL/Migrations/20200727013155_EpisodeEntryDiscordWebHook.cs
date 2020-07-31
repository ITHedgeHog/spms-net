using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.MSSQL.Migrations
{
    public partial class EpisodeEntryDiscordWebHook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPostedToDiscord",
                table: "EpisodeEntry",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPostedToDiscord",
                table: "EpisodeEntry");
        }
    }
}
