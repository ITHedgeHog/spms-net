using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class LinkEpisodeToSeries2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EpisodeStatus_Episode_EpisodeId",
                table: "EpisodeStatus");

            migrationBuilder.DropIndex(
                name: "IX_EpisodeStatus_EpisodeId",
                table: "EpisodeStatus");

            migrationBuilder.DropColumn(
                name: "EpisodeId",
                table: "EpisodeStatus");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Episode",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episode_StatusId",
                table: "Episode",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_EpisodeStatus_StatusId",
                table: "Episode",
                column: "StatusId",
                principalTable: "EpisodeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode_EpisodeStatus_StatusId",
                table: "Episode");

            migrationBuilder.DropIndex(
                name: "IX_Episode_StatusId",
                table: "Episode");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Episode");

            migrationBuilder.AddColumn<int>(
                name: "EpisodeId",
                table: "EpisodeStatus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeStatus_EpisodeId",
                table: "EpisodeStatus",
                column: "EpisodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EpisodeStatus_Episode_EpisodeId",
                table: "EpisodeStatus",
                column: "EpisodeId",
                principalTable: "Episode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
