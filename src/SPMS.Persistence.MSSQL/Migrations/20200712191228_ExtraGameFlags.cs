using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.MSSQL.Migrations
{
    public partial class ExtraGameFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTest",
                table: "Game",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Uuid",
                table: "Game",
                nullable: false,
                defaultValueSql: "NEWID()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTest",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Game");
        }
    }
}
