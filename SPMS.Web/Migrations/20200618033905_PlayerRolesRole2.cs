using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Web.Migrations
{
    public partial class PlayerRolesRole2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerRole_Player_PlayerId",
                table: "PlayerRole");

            migrationBuilder.DropIndex(
                name: "IX_PlayerRole_PlayerId",
                table: "PlayerRole");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayerRole");

            migrationBuilder.CreateTable(
                name: "PlayerRolePlayer",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    PlayerRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRolePlayer", x => new { x.PlayerId, x.PlayerRoleId });
                    table.ForeignKey(
                        name: "FK_PlayerRolePlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerRolePlayer_PlayerRole_PlayerRoleId",
                        column: x => x.PlayerRoleId,
                        principalTable: "PlayerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRolePlayer_PlayerRoleId",
                table: "PlayerRolePlayer",
                column: "PlayerRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerRolePlayer");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "PlayerRole",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRole_PlayerId",
                table: "PlayerRole",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerRole_Player_PlayerId",
                table: "PlayerRole",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
