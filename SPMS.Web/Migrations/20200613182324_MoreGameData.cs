using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Web.Migrations
{
    public partial class MoreGameData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostingId",
                table: "Biography",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Posting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posting", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biography_PostingId",
                table: "Biography",
                column: "PostingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Biography_Posting_PostingId",
                table: "Biography",
                column: "PostingId",
                principalTable: "Posting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Biography_Posting_PostingId",
                table: "Biography");

            migrationBuilder.DropTable(
                name: "Posting");

            migrationBuilder.DropIndex(
                name: "IX_Biography_PostingId",
                table: "Biography");

            migrationBuilder.DropColumn(
                name: "PostingId",
                table: "Biography");
        }
    }
}
