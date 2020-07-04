using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.PostgreSQL.Migrations
{
    public partial class BiographyStateToItemList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BiographyState",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "BiographyState",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "BiographyState",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BiographyState_GameId",
                table: "BiographyState",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BiographyState_Game_GameId",
                table: "BiographyState",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BiographyState_Game_GameId",
                table: "BiographyState");

            migrationBuilder.DropIndex(
                name: "IX_BiographyState_GameId",
                table: "BiographyState");

            migrationBuilder.DropColumn(
                name: "Default",
                table: "BiographyState");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "BiographyState");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BiographyState",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
