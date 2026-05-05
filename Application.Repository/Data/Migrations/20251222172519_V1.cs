using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassAbsent_Classes_ClassId",
                table: "ClassAbsent");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAbsent_TeacherClasses_TeacherClassesId",
                table: "ClassAbsent");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAbsent_Teacher_TeacherId",
                table: "ClassAbsent");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAbsent_Week_WeekId",
                table: "ClassAbsent");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassExamScore_TeacherClasses_TeacherClassesId",
                table: "ClassExamScore");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassScore_TeacherClasses_TeacherClassesId",
                table: "ClassScore");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassScore_Week_WeekId",
                table: "ClassScore");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Classes_ClassId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAbsentData_Student_StudentId",
                table: "StudentAbsentData");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExamScoreData_Student_StudentId",
                table: "StudentExamScoreData");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentScoreEdit_StudentScoreData_StudentScoreDataId",
                table: "StudentScoreEdit");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentScoreEdit_Student_StudentId",
                table: "StudentScoreEdit");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentScoreEdit_Teacher_TeacherId",
                table: "StudentScoreEdit");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Role_RoleId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherClasses_Subject_SubjectId",
                table: "TeacherClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherClasses_Teacher_TeacherId",
                table: "TeacherClasses");

            migrationBuilder.AlterColumn<long>(
                name: "TeacherId",
                table: "TeacherSupervisor",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SubjectId",
                table: "TeacherSupervisor",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SchoolId",
                table: "TeacherSupervisor",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "TeacherId",
                table: "TeacherClasses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SubjectId",
                table: "TeacherClasses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ClassId",
                table: "TeacherClasses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SchoolId",
                table: "Teacher",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "Teacher",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<long>(
                name: "TeacherId",
                table: "StudentScoreEdit",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "StudentScoreDataId",
                table: "StudentScoreEdit",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "StudentScoreEdit",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ClassScoreId",
                table: "StudentScoreData",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ClassExamScoreId",
                table: "StudentExamScoreData",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "StudentAbsentData",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ClassAbsentId",
                table: "StudentAbsentData",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "StudentAbsentCases",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ClassId",
                table: "Student",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "SendStudentToSchool",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SchoolSenderId",
                table: "SendStudentToSchool",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SchoolReceiverId",
                table: "SendStudentToSchool",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SchoolId",
                table: "SchoolVacation",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "AdministrationId",
                table: "School",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "WeekId",
                table: "ClassScore",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "TeacherClassesId",
                table: "ClassScore",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "TeacherClassesId",
                table: "ClassExamScore",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "SchoolId",
                table: "Classes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "WeekId",
                table: "ClassAbsent",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "AdministrationId",
                table: "AdministrationsVacation",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAbsent_Classes_ClassId",
                table: "ClassAbsent",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAbsent_TeacherClasses_TeacherClassesId",
                table: "ClassAbsent",
                column: "TeacherClassesId",
                principalTable: "TeacherClasses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAbsent_Teacher_TeacherId",
                table: "ClassAbsent",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAbsent_Week_WeekId",
                table: "ClassAbsent",
                column: "WeekId",
                principalTable: "Week",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassExamScore_TeacherClasses_TeacherClassesId",
                table: "ClassExamScore",
                column: "TeacherClassesId",
                principalTable: "TeacherClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassScore_TeacherClasses_TeacherClassesId",
                table: "ClassScore",
                column: "TeacherClassesId",
                principalTable: "TeacherClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassScore_Week_WeekId",
                table: "ClassScore",
                column: "WeekId",
                principalTable: "Week",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Classes_ClassId",
                table: "Student",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAbsentData_Student_StudentId",
                table: "StudentAbsentData",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExamScoreData_Student_StudentId",
                table: "StudentExamScoreData",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScoreEdit_StudentScoreData_StudentScoreDataId",
                table: "StudentScoreEdit",
                column: "StudentScoreDataId",
                principalTable: "StudentScoreData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScoreEdit_Student_StudentId",
                table: "StudentScoreEdit",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScoreEdit_Teacher_TeacherId",
                table: "StudentScoreEdit",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Role_RoleId",
                table: "Teacher",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherClasses_Subject_SubjectId",
                table: "TeacherClasses",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherClasses_Teacher_TeacherId",
                table: "TeacherClasses",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassAbsent_Classes_ClassId",
                table: "ClassAbsent");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAbsent_TeacherClasses_TeacherClassesId",
                table: "ClassAbsent");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAbsent_Teacher_TeacherId",
                table: "ClassAbsent");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassAbsent_Week_WeekId",
                table: "ClassAbsent");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassExamScore_TeacherClasses_TeacherClassesId",
                table: "ClassExamScore");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassScore_TeacherClasses_TeacherClassesId",
                table: "ClassScore");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassScore_Week_WeekId",
                table: "ClassScore");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Classes_ClassId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAbsentData_Student_StudentId",
                table: "StudentAbsentData");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentExamScoreData_Student_StudentId",
                table: "StudentExamScoreData");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentScoreEdit_StudentScoreData_StudentScoreDataId",
                table: "StudentScoreEdit");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentScoreEdit_Student_StudentId",
                table: "StudentScoreEdit");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentScoreEdit_Teacher_TeacherId",
                table: "StudentScoreEdit");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Role_RoleId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherClasses_Subject_SubjectId",
                table: "TeacherClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherClasses_Teacher_TeacherId",
                table: "TeacherClasses");

            migrationBuilder.AlterColumn<long>(
                name: "TeacherId",
                table: "TeacherSupervisor",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubjectId",
                table: "TeacherSupervisor",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SchoolId",
                table: "TeacherSupervisor",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TeacherId",
                table: "TeacherClasses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SubjectId",
                table: "TeacherClasses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClassId",
                table: "TeacherClasses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SchoolId",
                table: "Teacher",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "Teacher",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TeacherId",
                table: "StudentScoreEdit",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StudentScoreDataId",
                table: "StudentScoreEdit",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "StudentScoreEdit",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClassScoreId",
                table: "StudentScoreData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClassExamScoreId",
                table: "StudentExamScoreData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "StudentAbsentData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClassAbsentId",
                table: "StudentAbsentData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "StudentAbsentCases",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClassId",
                table: "Student",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StudentId",
                table: "SendStudentToSchool",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SchoolSenderId",
                table: "SendStudentToSchool",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SchoolReceiverId",
                table: "SendStudentToSchool",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SchoolId",
                table: "SchoolVacation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AdministrationId",
                table: "School",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WeekId",
                table: "ClassScore",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TeacherClassesId",
                table: "ClassScore",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TeacherClassesId",
                table: "ClassExamScore",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SchoolId",
                table: "Classes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WeekId",
                table: "ClassAbsent",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AdministrationId",
                table: "AdministrationsVacation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAbsent_Classes_ClassId",
                table: "ClassAbsent",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAbsent_TeacherClasses_TeacherClassesId",
                table: "ClassAbsent",
                column: "TeacherClassesId",
                principalTable: "TeacherClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAbsent_Teacher_TeacherId",
                table: "ClassAbsent",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassAbsent_Week_WeekId",
                table: "ClassAbsent",
                column: "WeekId",
                principalTable: "Week",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassExamScore_TeacherClasses_TeacherClassesId",
                table: "ClassExamScore",
                column: "TeacherClassesId",
                principalTable: "TeacherClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassScore_TeacherClasses_TeacherClassesId",
                table: "ClassScore",
                column: "TeacherClassesId",
                principalTable: "TeacherClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassScore_Week_WeekId",
                table: "ClassScore",
                column: "WeekId",
                principalTable: "Week",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Classes_ClassId",
                table: "Student",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAbsentData_Student_StudentId",
                table: "StudentAbsentData",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentExamScoreData_Student_StudentId",
                table: "StudentExamScoreData",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScoreEdit_StudentScoreData_StudentScoreDataId",
                table: "StudentScoreEdit",
                column: "StudentScoreDataId",
                principalTable: "StudentScoreData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScoreEdit_Student_StudentId",
                table: "StudentScoreEdit",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentScoreEdit_Teacher_TeacherId",
                table: "StudentScoreEdit",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Role_RoleId",
                table: "Teacher",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherClasses_Subject_SubjectId",
                table: "TeacherClasses",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherClasses_Teacher_TeacherId",
                table: "TeacherClasses",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
