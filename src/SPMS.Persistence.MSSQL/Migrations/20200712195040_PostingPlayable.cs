using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.MSSQL.Migrations
{
    public partial class PostingPlayable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlayable",
                table: "Posting",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlayable",
                table: "Posting");
        }
    }
}
