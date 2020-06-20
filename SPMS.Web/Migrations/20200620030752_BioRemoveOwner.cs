using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Web.Migrations
{
    public partial class BioRemoveOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Biography");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Biography",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
