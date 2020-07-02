using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.PostgreSQL.Migrations
{
    public partial class BiographyState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_BiographyState_StateId",
                table: "Biography");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Biography",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Biography_StatusId",
                table: "Biography",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_BiographyState_StateId",
                table: "Biography",
                column: "StateId",
                principalTable: "BiographyState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Biography_BiographyState_StateId",
                table: "Biography");

            migrationBuilder.DropForeignKey(
                name: "FK_Biography_BiographyStatus_StatusId",
                table: "Biography");

            migrationBuilder.DropIndex(
                name: "IX_Biography_StatusId",
                table: "Biography");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Biography",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_BiographyState_StateId",
                table: "Biography",
                column: "StateId",
                principalTable: "BiographyState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
