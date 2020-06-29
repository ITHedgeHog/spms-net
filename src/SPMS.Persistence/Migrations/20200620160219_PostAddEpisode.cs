using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class PostAddEpisode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EpisodeEntry_Episode_EpisodeId",
                table: "EpisodeEntry");

            migrationBuilder.AlterColumn<int>(
                name: "EpisodeId",
                table: "EpisodeEntry",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EpisodeEntry_Episode_EpisodeId",
                table: "EpisodeEntry",
                column: "EpisodeId",
                principalTable: "Episode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EpisodeEntry_Episode_EpisodeId",
                table: "EpisodeEntry");

            migrationBuilder.AlterColumn<int>(
                name: "EpisodeId",
                table: "EpisodeEntry",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EpisodeEntry_Episode_EpisodeId",
                table: "EpisodeEntry",
                column: "EpisodeId",
                principalTable: "Episode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
