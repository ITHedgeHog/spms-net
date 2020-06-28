using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class EpisodeEntryStatusFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_EpisodeEntry_EpisodeEntryStatus_EpisodeEntryStatusId",
                table: "EpisodeEntry",
                column: "EpisodeEntryStatusId",
                principalTable: "EpisodeEntryStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EpisodeEntry_EpisodeEntryStatus_EpisodeEntryStatusId",
                table: "EpisodeEntry");
        }
    }
}
