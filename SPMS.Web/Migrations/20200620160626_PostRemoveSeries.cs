using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Web.Migrations
{
    public partial class PostRemoveSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EpisodeEntry_Series_SeriesId",
                table: "EpisodeEntry");

            migrationBuilder.DropIndex(
                name: "IX_EpisodeEntry_SeriesId",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "EpisodeEntry");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeriesId",
                table: "EpisodeEntry",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeEntry_SeriesId",
                table: "EpisodeEntry",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EpisodeEntry_Series_SeriesId",
                table: "EpisodeEntry",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
