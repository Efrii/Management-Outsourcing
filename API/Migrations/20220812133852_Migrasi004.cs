using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Migrasi004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Job_position",
                table: "TB_Interview",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Job_position",
                table: "TB_Interview");
        }
    }
}
