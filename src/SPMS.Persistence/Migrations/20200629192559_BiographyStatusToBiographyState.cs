using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class BiographyStatusToBiographyState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_BiographyState_StatusId",
                table: "Biography");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Series_SeriesId",
                table: "Episode");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode_EpisodeStatus_StatusId",
                table: "Episode");

            migrationBuilder.DropIndex(
                name: "IX_Biography_StatusId",
                table: "Biography");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BiographyState",
                table: "BiographyState");

            migrationBuilder.RenameTable(
                name: "BiographyState",
                newName: "BiographyStatus");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Episode",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SeriesId",
                table: "Episode",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Biography",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BiographyStatus",
                table: "BiographyStatus",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Biography_StateId",
                table: "Biography",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_BiographyStatus_StateId",
                table: "Biography",
                column: "StateId",
                principalTable: "BiographyStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Series_SeriesId",
                table: "Episode",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_EpisodeStatus_StatusId",
                table: "Episode",
                column: "StatusId",
                principalTable: "EpisodeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_BiographyStatus_StateId",
                table: "Biography");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Series_SeriesId",
                table: "Episode");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode_EpisodeStatus_StatusId",
                table: "Episode");

            migrationBuilder.DropIndex(
                name: "IX_Biography_StateId",
                table: "Biography");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BiographyStatus",
                table: "BiographyStatus");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Biography");

            migrationBuilder.RenameTable(
                name: "BiographyStatus",
                newName: "BiographyState");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Episode",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SeriesId",
                table: "Episode",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BiographyState",
                table: "BiographyState",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Biography_StatusId",
                table: "Biography",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_BiographyState_StatusId",
                table: "Biography",
                column: "StatusId",
                principalTable: "BiographyState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Series_SeriesId",
                table: "Episode",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_EpisodeStatus_StatusId",
                table: "Episode",
                column: "StatusId",
                principalTable: "EpisodeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
