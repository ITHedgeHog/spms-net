using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class EpisodeEntryStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EpisodeEntryStatusId",
                table: "EpisodeEntry",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "EpisodeEntryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeEntryStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeEntry_EpisodeEntryStatusId",
                table: "EpisodeEntry",
                column: "EpisodeEntryStatusId");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "EpisodeEntryStatus");

            migrationBuilder.DropIndex(
                name: "IX_EpisodeEntry_EpisodeEntryStatusId",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "EpisodeEntryStatusId",
                table: "EpisodeEntry");
        }
    }
}
