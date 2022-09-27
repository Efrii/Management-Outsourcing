using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Migrasi012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is_check",
                table: "TB_CV",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_check",
                table: "TB_CV");
        }
    }
}
