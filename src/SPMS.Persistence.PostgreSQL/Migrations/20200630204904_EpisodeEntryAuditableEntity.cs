using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.PostgreSQL.Migrations
{
    public partial class EpisodeEntryAuditableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "EpisodeEntry",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EpisodeEntry",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "EpisodeEntry",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "EpisodeEntry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "EpisodeEntry");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "EpisodeEntry");
        }
    }
}
