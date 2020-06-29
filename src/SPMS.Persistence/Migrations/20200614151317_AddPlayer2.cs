using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class AddPlayer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Biography",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Biography_PlayerId",
                table: "Biography",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_Player_PlayerId",
                table: "Biography",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_Player_PlayerId",
                table: "Biography");

            migrationBuilder.DropIndex(
                name: "IX_Biography_PlayerId",
                table: "Biography");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Biography");
        }
    }
}
