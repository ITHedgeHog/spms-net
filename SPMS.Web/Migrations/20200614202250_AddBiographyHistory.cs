using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Web.Migrations
{
    public partial class AddBiographyHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "History",
                table: "Biography",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "History",
                table: "Biography");
        }
    }
}
