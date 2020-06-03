using Microsoft.EntityFrameworkCore.Migrations;

namespace Varesin.Database.Migrations
{
    public partial class AddField_Interviewer_Member_Migation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InterviewerId",
                table: "Members",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterviewerId",
                table: "Members");
        }
    }
}
