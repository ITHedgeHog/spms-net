using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.PostgreSQL.Migrations
{
    public partial class RemoveDatesFromEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EpisodeEntry");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EpisodeEntry",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EpisodeEntry",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
