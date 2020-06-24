using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Web.Migrations
{
    public partial class PlayerConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerConnection",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(nullable: false),
                    UserAgent = table.Column<string>(nullable: true),
                    Connected = table.Column<bool>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerConnection", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_PlayerConnection_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerConnection_PlayerId",
                table: "PlayerConnection",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerConnection");
        }
    }
}
