using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BiographyState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiographyStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Biography",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<string>(nullable: true),
                    Species = table.Column<string>(nullable: true),
                    Homeworld = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Born = table.Column<string>(nullable: true),
                    Eyes = table.Column<string>(nullable: true),
                    Hair = table.Column<string>(nullable: true),
                    Height = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true),
                    Affiliation = table.Column<string>(nullable: true),
                    Assignment = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    RankImage = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biography", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biography_BiographyStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "BiographyState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biography_StatusId",
                table: "Biography",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biography");

            migrationBuilder.DropTable(
                name: "BiographyState");
        }
    }
}
