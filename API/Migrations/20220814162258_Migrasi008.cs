using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Migrasi008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Is_accepted",
                table: "TB_Interview",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Is_accepted",
                table: "TB_Interview",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
