using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotChocolateDemo.Migrations
{
    /// <inheritdoc />
    public partial class secondChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CourseDTOId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CourseDTOId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CourseDTOId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "CourseDTOStudentDTO",
                columns: table => new
                {
                    CorusesId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDTOStudentDTO", x => new { x.CorusesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_CourseDTOStudentDTO_Courses_CorusesId",
                        column: x => x.CorusesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseDTOStudentDTO_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseDTOStudentDTO_StudentsId",
                table: "CourseDTOStudentDTO",
                column: "StudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseDTOStudentDTO");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseDTOId",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseDTOId",
                table: "Students",
                column: "CourseDTOId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CourseDTOId",
                table: "Students",
                column: "CourseDTOId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
