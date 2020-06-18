using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Web.Migrations
{
    public partial class PlayerRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_BiographyStatus_StatusId",
                table: "Biography");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Biography",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PlayerRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerRole_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRole_PlayerId",
                table: "PlayerRole",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_BiographyStatus_StatusId",
                table: "Biography",
                column: "StatusId",
                principalTable: "BiographyStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_BiographyStatus_StatusId",
                table: "Biography");

            migrationBuilder.DropTable(
                name: "PlayerRole");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Biography",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_BiographyStatus_StatusId",
                table: "Biography",
                column: "StatusId",
                principalTable: "BiographyStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
