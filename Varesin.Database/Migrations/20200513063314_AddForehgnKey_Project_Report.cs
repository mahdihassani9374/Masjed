using Microsoft.EntityFrameworkCore.Migrations;

namespace Varesin.Database.Migrations
{
    public partial class AddForehgnKey_Project_Report : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Reports",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ProjectId",
                table: "Reports",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Projects_ProjectId",
                table: "Reports",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Projects_ProjectId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ProjectId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Projects");
        }
    }
}
