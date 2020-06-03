using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Varesin.Database.Migrations
{
    public partial class AddEntity_Member_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 200, nullable: false),
                    Field = table.Column<string>(maxLength: 200, nullable: false),
                    UniversityName = table.Column<string>(nullable: true),
                    EducationalBackground = table.Column<string>(nullable: true),
                    JihadiHistory = table.Column<string>(nullable: true),
                    Skill = table.Column<string>(nullable: true),
                    BirthDate = table.Column<string>(nullable: true),
                    SuggestedFreeTime = table.Column<string>(nullable: true),
                    WorkExperienceWithAgeGroup = table.Column<string>(nullable: true),
                    AttendOnFridays = table.Column<bool>(nullable: false),
                    IsSingle = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    WorkingGroupOfferId = table.Column<int>(nullable: false),
                    WorkingGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_WorkingGroups_WorkingGroupId",
                        column: x => x.WorkingGroupId,
                        principalTable: "WorkingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Members_WorkingGroups_WorkingGroupOfferId",
                        column: x => x.WorkingGroupOfferId,
                        principalTable: "WorkingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_WorkingGroupId",
                table: "Members",
                column: "WorkingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_WorkingGroupOfferId",
                table: "Members",
                column: "WorkingGroupOfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
