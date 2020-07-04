using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.PostgreSQL.Migrations
{
    public partial class BiographyTypeRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_BiographyTypes_BiographyTypeId",
                table: "Biography");

            migrationBuilder.DropIndex(
                name: "IX_Biography_BiographyTypeId",
                table: "Biography");

            migrationBuilder.DropColumn(
                name: "BiographyTypeId",
                table: "Biography");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Biography",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Biography_TypeId",
                table: "Biography",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_BiographyTypes_TypeId",
                table: "Biography",
                column: "TypeId",
                principalTable: "BiographyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_BiographyTypes_TypeId",
                table: "Biography");

            migrationBuilder.DropIndex(
                name: "IX_Biography_TypeId",
                table: "Biography");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Biography");

            migrationBuilder.AddColumn<int>(
                name: "BiographyTypeId",
                table: "Biography",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Biography_BiographyTypeId",
                table: "Biography",
                column: "BiographyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_BiographyTypes_BiographyTypeId",
                table: "Biography",
                column: "BiographyTypeId",
                principalTable: "BiographyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
