using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Migrasi001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Acceptment",
                columns: table => new
                {
                    Id_Acceptment = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Jobs = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Acceptment", x => x.Id_Acceptment);
                });

            migrationBuilder.CreateTable(
                name: "TB_Company",
                columns: table => new
                {
                    Id_company = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_company = table.Column<string>(nullable: false),
                    Email_company = table.Column<string>(nullable: false),
                    Address_company = table.Column<string>(nullable: false),
                    Phone_company = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Company", x => x.Id_company);
                });

            migrationBuilder.CreateTable(
                name: "TB_Role",
                columns: table => new
                {
                    Id_role = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_role = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Role", x => x.Id_role);
                });

            migrationBuilder.CreateTable(
                name: "TB_Employee",
                columns: table => new
                {
                    Id_employee = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_acceptment = table.Column<int>(nullable: true),
                    Id_manager = table.Column<int>(nullable: true),
                    Id_trainner = table.Column<int>(nullable: true),
                    Name_employee = table.Column<string>(nullable: false),
                    Email_employee = table.Column<string>(nullable: false),
                    Nik_employee = table.Column<string>(nullable: false),
                    Datebirth = table.Column<DateTime>(nullable: false),
                    Age_employee = table.Column<string>(nullable: false),
                    Gender_Employee = table.Column<string>(nullable: false),
                    Phone_number = table.Column<string>(nullable: false),
                    Is_place = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Employee", x => x.Id_employee);
                    table.ForeignKey(
                        name: "FK_TB_Employee_TB_Acceptment_Id_acceptment",
                        column: x => x.Id_acceptment,
                        principalTable: "TB_Acceptment",
                        principalColumn: "Id_Acceptment",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_Employee_TB_Employee_Id_manager",
                        column: x => x.Id_manager,
                        principalTable: "TB_Employee",
                        principalColumn: "Id_employee",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_Employee_TB_Employee_Id_trainner",
                        column: x => x.Id_trainner,
                        principalTable: "TB_Employee",
                        principalColumn: "Id_employee",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Jobs",
                columns: table => new
                {
                    Id_jobs = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_company = table.Column<int>(nullable: false),
                    Name_jobs = table.Column<string>(nullable: false),
                    Recruitment_skill = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Jobs", x => x.Id_jobs);
                    table.ForeignKey(
                        name: "FK_TB_Jobs_TB_Company_Id_company",
                        column: x => x.Id_company,
                        principalTable: "TB_Company",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_CV",
                columns: table => new
                {
                    Id_cv = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_employee = table.Column<int>(nullable: false),
                    Id_company = table.Column<int>(nullable: false),
                    Cv_employee = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CV", x => x.Id_cv);
                    table.ForeignKey(
                        name: "FK_TB_CV_TB_Company_Id_company",
                        column: x => x.Id_company,
                        principalTable: "TB_Company",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_CV_TB_Employee_Id_employee",
                        column: x => x.Id_employee,
                        principalTable: "TB_Employee",
                        principalColumn: "Id_employee",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Interview",
                columns: table => new
                {
                    Id_interview = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_employee = table.Column<int>(nullable: false),
                    Id_company = table.Column<int>(nullable: false),
                    Date_interview = table.Column<DateTime>(nullable: false),
                    Url_Intertview = table.Column<string>(nullable: false),
                    Interviewer_name = table.Column<string>(nullable: false),
                    Is_done = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Interview", x => x.Id_interview);
                    table.ForeignKey(
                        name: "FK_TB_Interview_TB_Company_Id_company",
                        column: x => x.Id_company,
                        principalTable: "TB_Company",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Interview_TB_Employee_Id_employee",
                        column: x => x.Id_employee,
                        principalTable: "TB_Employee",
                        principalColumn: "Id_employee",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Score",
                columns: table => new
                {
                    Id_score = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_employee = table.Column<int>(nullable: false),
                    Segment1 = table.Column<int>(nullable: false),
                    Segment2 = table.Column<int>(nullable: false),
                    Segment3 = table.Column<int>(nullable: false),
                    Segment4 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Score", x => x.Id_score);
                    table.ForeignKey(
                        name: "FK_TB_Score_TB_Employee_Id_employee",
                        column: x => x.Id_employee,
                        principalTable: "TB_Employee",
                        principalColumn: "Id_employee",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_User",
                columns: table => new
                {
                    Id_User = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_employee = table.Column<int>(nullable: true),
                    Id_company = table.Column<int>(nullable: true),
                    Username = table.Column<string>(maxLength: 12, nullable: false),
                    Password = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_User", x => x.Id_User);
                    table.ForeignKey(
                        name: "FK_TB_User_TB_Company_Id_company",
                        column: x => x.Id_company,
                        principalTable: "TB_Company",
                        principalColumn: "Id_company",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_User_TB_Employee_Id_employee",
                        column: x => x.Id_employee,
                        principalTable: "TB_Employee",
                        principalColumn: "Id_employee",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_UserRole",
                columns: table => new
                {
                    Id_UserRole = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_user = table.Column<int>(nullable: false),
                    Id_role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_UserRole", x => x.Id_UserRole);
                    table.ForeignKey(
                        name: "FK_TB_UserRole_TB_Role_Id_role",
                        column: x => x.Id_role,
                        principalTable: "TB_Role",
                        principalColumn: "Id_role",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_UserRole_TB_User_Id_user",
                        column: x => x.Id_user,
                        principalTable: "TB_User",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CV_Id_company",
                table: "TB_CV",
                column: "Id_company");

            migrationBuilder.CreateIndex(
                name: "IX_TB_CV_Id_employee",
                table: "TB_CV",
                column: "Id_employee");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Employee_Id_acceptment",
                table: "TB_Employee",
                column: "Id_acceptment");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Employee_Id_manager",
                table: "TB_Employee",
                column: "Id_manager");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Employee_Id_trainner",
                table: "TB_Employee",
                column: "Id_trainner");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Interview_Id_company",
                table: "TB_Interview",
                column: "Id_company");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Interview_Id_employee",
                table: "TB_Interview",
                column: "Id_employee");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Jobs_Id_company",
                table: "TB_Jobs",
                column: "Id_company");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Score_Id_employee",
                table: "TB_Score",
                column: "Id_employee");

            migrationBuilder.CreateIndex(
                name: "IX_TB_User_Id_company",
                table: "TB_User",
                column: "Id_company");

            migrationBuilder.CreateIndex(
                name: "IX_TB_User_Id_employee",
                table: "TB_User",
                column: "Id_employee");

            migrationBuilder.CreateIndex(
                name: "IX_TB_UserRole_Id_role",
                table: "TB_UserRole",
                column: "Id_role");

            migrationBuilder.CreateIndex(
                name: "IX_TB_UserRole_Id_user",
                table: "TB_UserRole",
                column: "Id_user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CV");

            migrationBuilder.DropTable(
                name: "TB_Interview");

            migrationBuilder.DropTable(
                name: "TB_Jobs");

            migrationBuilder.DropTable(
                name: "TB_Score");

            migrationBuilder.DropTable(
                name: "TB_UserRole");

            migrationBuilder.DropTable(
                name: "TB_Role");

            migrationBuilder.DropTable(
                name: "TB_User");

            migrationBuilder.DropTable(
                name: "TB_Company");

            migrationBuilder.DropTable(
                name: "TB_Employee");

            migrationBuilder.DropTable(
                name: "TB_Acceptment");
        }
    }
}
