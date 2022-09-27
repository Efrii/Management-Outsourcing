using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Migrasi011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Employee_TB_Acceptment_Id_acceptment",
                table: "TB_Employee");

            migrationBuilder.DropIndex(
                name: "IX_TB_Employee_Id_acceptment",
                table: "TB_Employee");

            migrationBuilder.DropColumn(
                name: "Id_acceptment",
                table: "TB_Employee");

            migrationBuilder.AddColumn<int>(
                name: "Id_employee",
                table: "TB_Acceptment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Acceptment_Id_employee",
                table: "TB_Acceptment",
                column: "Id_employee");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Acceptment_TB_Employee_Id_employee",
                table: "TB_Acceptment",
                column: "Id_employee",
                principalTable: "TB_Employee",
                principalColumn: "Id_employee",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Acceptment_TB_Employee_Id_employee",
                table: "TB_Acceptment");

            migrationBuilder.DropIndex(
                name: "IX_TB_Acceptment_Id_employee",
                table: "TB_Acceptment");

            migrationBuilder.DropColumn(
                name: "Id_employee",
                table: "TB_Acceptment");

            migrationBuilder.AddColumn<int>(
                name: "Id_acceptment",
                table: "TB_Employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Employee_Id_acceptment",
                table: "TB_Employee",
                column: "Id_acceptment");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Employee_TB_Acceptment_Id_acceptment",
                table: "TB_Employee",
                column: "Id_acceptment",
                principalTable: "TB_Acceptment",
                principalColumn: "Id_Acceptment",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
