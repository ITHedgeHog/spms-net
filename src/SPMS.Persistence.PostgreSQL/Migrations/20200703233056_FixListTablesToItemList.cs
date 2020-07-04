using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.PostgreSQL.Migrations
{
    public partial class FixListTablesToItemList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Posting",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "Posting",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Posting",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BiographyStatus",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "BiographyStatus",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "BiographyStatus",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posting_GameId",
                table: "Posting",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_BiographyStatus_GameId",
                table: "BiographyStatus",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BiographyStatus_Game_GameId",
                table: "BiographyStatus",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posting_Game_GameId",
                table: "Posting",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BiographyStatus_Game_GameId",
                table: "BiographyStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_Posting_Game_GameId",
                table: "Posting");

            migrationBuilder.DropIndex(
                name: "IX_Posting_GameId",
                table: "Posting");

            migrationBuilder.DropIndex(
                name: "IX_BiographyStatus_GameId",
                table: "BiographyStatus");

            migrationBuilder.DropColumn(
                name: "Default",
                table: "Posting");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Posting");

            migrationBuilder.DropColumn(
                name: "Default",
                table: "BiographyStatus");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "BiographyStatus");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Posting",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BiographyStatus",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
