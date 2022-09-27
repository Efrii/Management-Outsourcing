using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Migrasi009 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name_Jobs",
                table: "TB_Acceptment");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "TB_Acceptment");

            migrationBuilder.AddColumn<string>(
                name: "Job_position",
                table: "TB_Acceptment",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name_Company",
                table: "TB_Acceptment",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Job_position",
                table: "TB_Acceptment");

            migrationBuilder.DropColumn(
                name: "Name_Company",
                table: "TB_Acceptment");

            migrationBuilder.AddColumn<string>(
                name: "Name_Jobs",
                table: "TB_Acceptment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "TB_Acceptment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
