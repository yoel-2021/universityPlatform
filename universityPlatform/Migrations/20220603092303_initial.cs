using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace universityPlatform.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "coursesId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "categoriesId",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "studentsId",
                table: "Course",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "coursesId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "categoriesId",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "studentsId",
                table: "Course");
        }
    }
}
