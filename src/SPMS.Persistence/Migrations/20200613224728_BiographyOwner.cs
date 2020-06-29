using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.Migrations
{
    public partial class BiographyOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Biography",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Biography");
        }
    }
}
