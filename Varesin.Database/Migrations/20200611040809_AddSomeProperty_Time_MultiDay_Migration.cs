using Microsoft.EntityFrameworkCore.Migrations;

namespace Varesin.Database.Migrations
{
    public partial class AddSomeProperty_Time_MultiDay_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MultiDay",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Events",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MultiDay",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Events");
        }
    }
}
