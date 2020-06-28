using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class EpisodeEntryPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_EpisodeEntry_EpisodeEntryId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_EpisodeEntryId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "EpisodeEntryId",
                table: "Player");

            migrationBuilder.CreateTable(
                name: "EpisodeEntryPlayer",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    EpisodeEntryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeEntryPlayer", x => new { x.PlayerId, x.EpisodeEntryId });
                    table.ForeignKey(
                        name: "FK_EpisodeEntryPlayer_EpisodeEntry_EpisodeEntryId",
                        column: x => x.EpisodeEntryId,
                        principalTable: "EpisodeEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EpisodeEntryPlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeEntryPlayer_EpisodeEntryId",
                table: "EpisodeEntryPlayer",
                column: "EpisodeEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EpisodeEntryPlayer");

            migrationBuilder.AddColumn<int>(
                name: "EpisodeEntryId",
                table: "Player",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_EpisodeEntryId",
                table: "Player",
                column: "EpisodeEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_EpisodeEntry_EpisodeEntryId",
                table: "Player",
                column: "EpisodeEntryId",
                principalTable: "EpisodeEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
