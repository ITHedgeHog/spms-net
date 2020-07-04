using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SPMS.Persistence.PostgreSQL.Migrations
{
    public partial class BiographyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BiographyTypeId",
                table: "Biography",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BiographyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    GameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiographyTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiographyTypes_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biography_BiographyTypeId",
                table: "Biography",
                column: "BiographyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BiographyTypes_GameId",
                table: "BiographyTypes",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_BiographyTypes_BiographyTypeId",
                table: "Biography",
                column: "BiographyTypeId",
                principalTable: "BiographyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_BiographyTypes_BiographyTypeId",
                table: "Biography");

            migrationBuilder.DropTable(
                name: "BiographyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Biography_BiographyTypeId",
                table: "Biography");

            migrationBuilder.DropColumn(
                name: "BiographyTypeId",
                table: "Biography");
        }
    }
}
