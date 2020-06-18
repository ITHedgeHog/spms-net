using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Web.Migrations
{
    public partial class PostAuthoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EpisodeEntryTypeId",
                table: "EpisodeEntry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "EpisodeEntry",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriesId",
                table: "EpisodeEntry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Timeline",
                table: "EpisodeEntry",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EpisodeEntryType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeEntryType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeEntry_EpisodeEntryTypeId",
                table: "EpisodeEntry",
                column: "EpisodeEntryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeEntry_SeriesId",
                table: "EpisodeEntry",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EpisodeEntry_EpisodeEntryType_EpisodeEntryTypeId",
                table: "EpisodeEntry",
                column: "EpisodeEntryTypeId",
                principalTable: "EpisodeEntryType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EpisodeEntry_Series_SeriesId",
                table: "EpisodeEntry",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EpisodeEntry_EpisodeEntryType_EpisodeEntryTypeId",
                table: "EpisodeEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_EpisodeEntry_Series_SeriesId",
                table: "EpisodeEntry");

            migrationBuilder.DropTable(
                name: "EpisodeEntryType");

            migrationBuilder.DropIndex(
                name: "IX_EpisodeEntry_EpisodeEntryTypeId",
                table: "EpisodeEntry");

            migrationBuilder.DropIndex(
                name: "IX_EpisodeEntry_SeriesId",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "EpisodeEntryTypeId",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "Timeline",
                table: "EpisodeEntry");
        }
    }
}
