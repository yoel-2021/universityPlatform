using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace universityPlatform.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriesCourses");

            migrationBuilder.DropTable(
                name: "StudentsCourses");

            migrationBuilder.AddColumn<int>(
                name: "coursesId",
                table: "Category",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "coursesId",
                table: "Category");

            migrationBuilder.CreateTable(
                name: "CategoriesCourses",
                columns: table => new
                {
                    courseId = table.Column<int>(type: "int", nullable: false),
                    categoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesCourses", x => new { x.courseId, x.categoriesId });
                    table.ForeignKey(
                        name: "FK_CategoriesCourses_Category_categoriesId",
                        column: x => x.categoriesId,
                        principalTable: "Category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriesCourses_Course_courseId",
                        column: x => x.courseId,
                        principalTable: "Course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentsCourses",
                columns: table => new
                {
                    coursesId = table.Column<int>(type: "int", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    studentsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsCourses", x => new { x.coursesId, x.studentId });
                    table.ForeignKey(
                        name: "FK_StudentsCourses_Course_coursesId",
                        column: x => x.coursesId,
                        principalTable: "Course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsCourses_Student_studentsid",
                        column: x => x.studentsid,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesCourses_categoriesId",
                table: "CategoriesCourses",
                column: "categoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCourses_studentsid",
                table: "StudentsCourses",
                column: "studentsid");
        }
    }
}
