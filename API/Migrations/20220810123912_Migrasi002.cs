using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Migrasi002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_class",
                table: "TB_Employee",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id_class = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_class = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id_class);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Employee_Id_class",
                table: "TB_Employee",
                column: "Id_class");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Employee_Classes_Id_class",
                table: "TB_Employee",
                column: "Id_class",
                principalTable: "Classes",
                principalColumn: "Id_class",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Employee_Classes_Id_class",
                table: "TB_Employee");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_TB_Employee_Id_class",
                table: "TB_Employee");

            migrationBuilder.DropColumn(
                name: "Id_class",
                table: "TB_Employee");
        }
    }
}
