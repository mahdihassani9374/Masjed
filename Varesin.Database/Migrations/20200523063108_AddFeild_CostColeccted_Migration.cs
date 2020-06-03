using Microsoft.EntityFrameworkCore.Migrations;

namespace Varesin.Database.Migrations
{
    public partial class AddFeild_CostColeccted_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostCollected",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCollected",
                table: "Projects");
        }
    }
}
