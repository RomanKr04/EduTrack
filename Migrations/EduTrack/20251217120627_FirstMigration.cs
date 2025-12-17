using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EduTrack.Migrations.EduTrack
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "students",
            //    columns: table => new
            //    {
            //        student_id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        username = table.Column<string>(type: "text", nullable: false),
            //        password = table.Column<string>(type: "text", nullable: false),
            //        first_name = table.Column<string>(type: "text", nullable: false),
            //        last_name = table.Column<string>(type: "text", nullable: false),
            //        email = table.Column<string>(type: "text", nullable: false),
            //        role = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_students", x => x.student_id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "courses",
            //    columns: table => new
            //    {
            //        course_id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        course_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            //        description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
            //        student_id = table.Column<int>(type: "integer", nullable: false),
            //        join_code = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_courses", x => x.course_id);
            //        table.ForeignKey(
            //            name: "FK_courses_students_student_id",
            //            column: x => x.student_id,
            //            principalTable: "students",
            //            principalColumn: "student_id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "assignments",
            //    columns: table => new
            //    {
            //        assignment_id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
            //        description = table.Column<string>(type: "text", nullable: false),
            //        course_id = table.Column<int>(type: "integer", nullable: false),
            //        due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //        file = table.Column<byte[]>(type: "bytea", nullable: false),
            //        file_name = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_assignments", x => x.assignment_id);
            //        table.ForeignKey(
            //            name: "FK_assignments_courses_course_id",
            //            column: x => x.course_id,
            //            principalTable: "courses",
            //            principalColumn: "course_id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "course_assignments",
                columns: table => new
                {
                    assignment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_assignments", x => x.assignment_id);
                    table.ForeignKey(
                        name: "FK_course_assignments_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_assignments_students_student_id",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "submissions",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    assignment_id = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<int>(type: "integer", nullable: false),
                    submission_date = table.Column<DateTime>(type: "date", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    file = table.Column<byte[]>(type: "bytea", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submissions", x => x.submission_id);
                    table.ForeignKey(
                        name: "FK_submissions_assignments_assignment_id",
                        column: x => x.assignment_id,
                        principalTable: "assignments",
                        principalColumn: "assignment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_submissions_students_student_id",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    submission_id = table.Column<int>(type: "integer", nullable: false),
                    grade = table.Column<decimal>(type: "numeric", nullable: true),
                    comments = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grades", x => x.grade_id);
                    table.ForeignKey(
                        name: "FK_grades_submissions_submission_id",
                        column: x => x.submission_id,
                        principalTable: "submissions",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_assignments_course_id",
            //    table: "assignments",
            //    column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_assignments_course_id",
                table: "course_assignments",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_assignments_student_id",
                table: "course_assignments",
                column: "student_id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_courses_student_id",
            //    table: "courses",
            //    column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_grades_submission_id",
                table: "grades",
                column: "submission_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_submissions_assignment_id",
                table: "submissions",
                column: "assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_submissions_student_id",
                table: "submissions",
                column: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_assignments");

            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropTable(
                name: "submissions");

            //migrationBuilder.DropTable(
            //    name: "assignments");

            //migrationBuilder.DropTable(
            //    name: "courses");

            //migrationBuilder.DropTable(
            //    name: "students");
        }
    }
}
