using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class AddPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Series",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(nullable: true),
                    AuthString = table.Column<string>(nullable: true),
                    EpisodeEntryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_EpisodeEntry_EpisodeEntryId",
                        column: x => x.EpisodeEntryId,
                        principalTable: "EpisodeEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Series_GameId",
                table: "Series",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Player_EpisodeEntryId",
                table: "Player",
                column: "EpisodeEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Game_GameId",
                table: "Series",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Game_GameId",
                table: "Series");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Series_GameId",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Series");
        }
    }
}
