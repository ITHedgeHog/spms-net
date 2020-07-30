using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.MSSQL.Migrations
{
    public partial class Bio2Game : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Biography",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Biography_GameId",
                table: "Biography",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_Game_GameId",
                table: "Biography",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_Game_GameId",
                table: "Biography");

            migrationBuilder.DropIndex(
                name: "IX_Biography_GameId",
                table: "Biography");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Biography");
        }
    }
}
