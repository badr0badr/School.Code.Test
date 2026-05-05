using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class V0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleArabic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanBeSelected = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Week",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Week", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdministrationsVacation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdministrationId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrationsVacation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministrationsVacation_Administrations_AdministrationId",
                        column: x => x.AdministrationId,
                        principalTable: "Administrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolMangerId = table.Column<long>(type: "bigint", nullable: true),
                    AdministrationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.Id);
                    table.ForeignKey(
                        name: "FK_School_Administrations_AdministrationId",
                        column: x => x.AdministrationId,
                        principalTable: "Administrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolVacation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolVacation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolVacation_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    MangerId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teacher_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teacher_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teacher_Teacher_MangerId",
                        column: x => x.MangerId,
                        principalTable: "Teacher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ClassId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherClasses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false),
                    ClassId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherClasses_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherClasses_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherClasses_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSupervisor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSupervisor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherSupervisor_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSupervisor_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSupervisor_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SendStudentToSchool",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolSenderId = table.Column<long>(type: "bigint", nullable: false),
                    SchoolReceiverId = table.Column<long>(type: "bigint", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    CanBeSend = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendStudentToSchool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendStudentToSchool_School_SchoolReceiverId",
                        column: x => x.SchoolReceiverId,
                        principalTable: "School",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SendStudentToSchool_School_SchoolSenderId",
                        column: x => x.SchoolSenderId,
                        principalTable: "School",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SendStudentToSchool_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAbsentCases",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAbsentCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAbsentCases_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassAbsent",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ClassNumber = table.Column<int>(type: "int", nullable: false),
                    WeekId = table.Column<long>(type: "bigint", nullable: false),
                    TeacherClassesId = table.Column<long>(type: "bigint", nullable: true),
                    TeacherId = table.Column<long>(type: "bigint", nullable: true),
                    ClassId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassAbsent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassAbsent_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassAbsent_TeacherClasses_TeacherClassesId",
                        column: x => x.TeacherClassesId,
                        principalTable: "TeacherClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassAbsent_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassAbsent_Week_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Week",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassExamScore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    TeacherClassesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassExamScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassExamScore_TeacherClasses_TeacherClassesId",
                        column: x => x.TeacherClassesId,
                        principalTable: "TeacherClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassScore",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    WeekId = table.Column<long>(type: "bigint", nullable: false),
                    TeacherClassesId = table.Column<long>(type: "bigint", nullable: false),
                    ClassesId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassScore_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassScore_TeacherClasses_TeacherClassesId",
                        column: x => x.TeacherClassesId,
                        principalTable: "TeacherClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassScore_Week_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Week",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAbsentData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    IsAbsent = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassAbsentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAbsentData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAbsentData_ClassAbsent_ClassAbsentId",
                        column: x => x.ClassAbsentId,
                        principalTable: "ClassAbsent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAbsentData_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentExamScoreData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassExamScoreId = table.Column<long>(type: "bigint", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: true),
                    ExamResult = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExamScoreData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExamScoreData_ClassExamScore_ClassExamScoreId",
                        column: x => x.ClassExamScoreId,
                        principalTable: "ClassExamScore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentExamScoreData_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentScoreData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassScoreId = table.Column<long>(type: "bigint", nullable: false),
                    StudentId = table.Column<long>(type: "bigint", nullable: true),
                    Review = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Behavior = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Homework = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentScoreData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentScoreData_ClassScore_ClassScoreId",
                        column: x => x.ClassScoreId,
                        principalTable: "ClassScore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentScoreData_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentScoreEdit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    TeacherId = table.Column<long>(type: "bigint", nullable: false),
                    StudentScoreDataId = table.Column<long>(type: "bigint", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentScoreEdit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentScoreEdit_StudentScoreData_StudentScoreDataId",
                        column: x => x.StudentScoreDataId,
                        principalTable: "StudentScoreData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentScoreEdit_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentScoreEdit_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdministrationsVacation_AdministrationId",
                table: "AdministrationsVacation",
                column: "AdministrationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAbsent_ClassId",
                table: "ClassAbsent",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAbsent_TeacherClassesId",
                table: "ClassAbsent",
                column: "TeacherClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAbsent_TeacherId",
                table: "ClassAbsent",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassAbsent_WeekId",
                table: "ClassAbsent",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SchoolId",
                table: "Classes",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassExamScore_TeacherClassesId",
                table: "ClassExamScore",
                column: "TeacherClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScore_ClassesId",
                table: "ClassScore",
                column: "ClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScore_TeacherClassesId",
                table: "ClassScore",
                column: "TeacherClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassScore_WeekId",
                table: "ClassScore",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_School_AdministrationId",
                table: "School",
                column: "AdministrationId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolVacation_SchoolId",
                table: "SchoolVacation",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SendStudentToSchool_SchoolReceiverId",
                table: "SendStudentToSchool",
                column: "SchoolReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_SendStudentToSchool_SchoolSenderId",
                table: "SendStudentToSchool",
                column: "SchoolSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_SendStudentToSchool_StudentId",
                table: "SendStudentToSchool",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassId",
                table: "Student",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAbsentCases_StudentId",
                table: "StudentAbsentCases",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAbsentData_ClassAbsentId",
                table: "StudentAbsentData",
                column: "ClassAbsentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAbsentData_StudentId",
                table: "StudentAbsentData",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamScoreData_ClassExamScoreId",
                table: "StudentExamScoreData",
                column: "ClassExamScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExamScoreData_StudentId",
                table: "StudentExamScoreData",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentScoreData_ClassScoreId",
                table: "StudentScoreData",
                column: "ClassScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentScoreData_StudentId",
                table: "StudentScoreData",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentScoreEdit_StudentId",
                table: "StudentScoreEdit",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentScoreEdit_StudentScoreDataId",
                table: "StudentScoreEdit",
                column: "StudentScoreDataId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentScoreEdit_TeacherId",
                table: "StudentScoreEdit",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_MangerId",
                table: "Teacher",
                column: "MangerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_RoleId",
                table: "Teacher",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_SchoolId",
                table: "Teacher",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClasses_ClassId",
                table: "TeacherClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClasses_SubjectId",
                table: "TeacherClasses",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClasses_TeacherId",
                table: "TeacherClasses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSupervisor_SchoolId",
                table: "TeacherSupervisor",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSupervisor_SubjectId",
                table: "TeacherSupervisor",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSupervisor_TeacherId",
                table: "TeacherSupervisor",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdministrationsVacation");

            migrationBuilder.DropTable(
                name: "SchoolVacation");

            migrationBuilder.DropTable(
                name: "SendStudentToSchool");

            migrationBuilder.DropTable(
                name: "StudentAbsentCases");

            migrationBuilder.DropTable(
                name: "StudentAbsentData");

            migrationBuilder.DropTable(
                name: "StudentExamScoreData");

            migrationBuilder.DropTable(
                name: "StudentScoreEdit");

            migrationBuilder.DropTable(
                name: "TeacherSupervisor");

            migrationBuilder.DropTable(
                name: "ClassAbsent");

            migrationBuilder.DropTable(
                name: "ClassExamScore");

            migrationBuilder.DropTable(
                name: "StudentScoreData");

            migrationBuilder.DropTable(
                name: "ClassScore");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "TeacherClasses");

            migrationBuilder.DropTable(
                name: "Week");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "Administrations");
        }
    }
}
