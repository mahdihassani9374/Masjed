using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Varesin.Database.Migrations
{
    public partial class AddEntity_Payment_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(maxLength: 300, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Price = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsSuccess = table.Column<bool>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    RecordId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
