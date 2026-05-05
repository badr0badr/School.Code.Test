using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Student
{
    public class AddStudentView
    {
        [Required(ErrorMessage = "ادخل اسم الطالب")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ادخل فصل الطالب")]
        public long ClassId { get; set; }
        [Required(ErrorMessage = "ادخل نوع الطالب")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "ادخل ديانة الطالب")]
        public string Religion { get; set; }
        [Required(ErrorMessage = "ادخل حالة الطالب")]
        public string Status { get; set; }
        public string? MargeType { get; set; }
    }
    public class EditStudentAbsentDaysView
    {
        public long StudentId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set; }
    }
    public class EditStudentView
    {
        public long Id { get; set; }
        public long? ClassId { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Religion { get; set; }
        public string? Status { get; set; }
        public string? MargeType { get; set; }
    }
    public class ClassStudentsView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Status { get; set; }
        public string MargeType { get; set; }
    }
    public class SendStudentToAnotherSchoolView
    {
        public long StudentId { get; set; }
        public long OldSchool { get; set; }
        public long NewSchool { get; set; }
    }
    public class GetTransformationsView
    {
        public long Id { get; set; }
        public string StudentName { get; set; }
        public string SchoolName { get; set; }
        public bool CanBeSend { get; set; }
    }
    public class AcceptStudentTransformationView
    {
        public long Id { get; set; }
        public long ClassId { get; set; }
    }
    public class GetAllStudentStaticsSchoolData
    {
        public string ReportName { get; set; }
        public long TotalStudents { get; set; } = 0;
        public long TotalMale { get; set; } = 0;
        public long TotalFemale { get; set; } = 0;
        public long TotalMuslim { get; set; } = 0;
        public long TotalCruchin { get; set; } = 0;
        public long TotalNew { get; set; } = 0;
        public long TotalStay { get; set; } = 0;
        public long TotalFreze { get; set; } = 0;
        public List<GetAllStudentStaticsLevelsData> Levels { get; set; } = new List<GetAllStudentStaticsLevelsData>();
    }
    public class GetAllStudentStaticsLevelsData    //اعدادي
    {
        public string LevelName { get; set; }
        public long TotalStudents { get; set; } = 0;
        public long TotalMale { get; set; } = 0;
        public long TotalFemale { get; set; } = 0;
        public long TotalMuslim { get; set; } = 0;
        public long TotalCruchin { get; set; } = 0;
        public long TotalNew { get; set; } = 0;
        public long TotalStay { get; set; } = 0;
        public long TotalFreze { get; set; } = 0;
        public List<GetAllStudentStaticsGradesData> Grades { get; set; } = new List<GetAllStudentStaticsGradesData>();
    }
    public class GetAllStudentStaticsGradesData //اولي اعدادي
    {
        public string GradeName { get; set; }
        public long TotalStudents { get; set; } = 0;
        public long TotalMale { get; set; } = 0;
        public long TotalFemale { get; set; } = 0;
        public long TotalMuslim { get; set; } = 0;
        public long TotalCruchin { get; set; } = 0;
        public long TotalNew { get; set; } = 0;
        public long TotalStay { get; set; } = 0;
        public long TotalFreze { get; set; } = 0;
        public List<GetAllStudentStaticsClassesData> Classes { get; set; } = new List<GetAllStudentStaticsClassesData>();
    }
    public class GetAllStudentStaticsClassesData    /////  1-1 ع
    {
        public long ClassId { get; set; }
        public string ClassName { get; set; }
        public long TotalStudents { get; set; } = 0;
        public long TotalMale { get; set; } = 0;
        public long TotalFemale { get; set; } = 0;
        public long TotalMuslim { get; set; } = 0;
        public long TotalCruchin { get; set; } = 0;
        public long TotalNew { get; set; } = 0;
        public long TotalStay { get; set; } = 0;
        public long TotalFreze { get; set; } = 0;
    }
    public class StudentCodeUpdate
    {
        public long Id { get; set; }
        public long Code { get; set; }
    }
    public class UploadedStudentNames
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsValid { get; set; }
    }
}
