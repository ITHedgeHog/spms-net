using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class EpisodeEntryDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "EpisodeEntry",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EpisodeEntry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EpisodeEntry");
        }
    }
}
