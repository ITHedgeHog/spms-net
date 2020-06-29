using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class PostingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_Posting_PostingId",
                table: "Biography");

            migrationBuilder.AlterColumn<int>(
                name: "PostingId",
                table: "Biography",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_Posting_PostingId",
                table: "Biography",
                column: "PostingId",
                principalTable: "Posting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_Posting_PostingId",
                table: "Biography");

            migrationBuilder.AlterColumn<int>(
                name: "PostingId",
                table: "Biography",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_Posting_PostingId",
                table: "Biography",
                column: "PostingId",
                principalTable: "Posting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
