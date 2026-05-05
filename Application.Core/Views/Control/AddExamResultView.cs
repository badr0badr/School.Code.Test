using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Control
{
    public class AddExamResultView
    {
        public int Code { get; set; }
        public double Score { get; set; }
    }
    public class AddToControlView
    {
        public long TeacherId { get; set; }
        public string ControlType { get; set; }
    }
    public class ControlTeachersData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ControlType { get; set; }
    }
    public class GenderSortMax
    {
        public int Counter { get; set; }
        public string Gender { get; set; }
        public string ClassName { get; set; }
        public List<Application.Core.Entities.Student> Students { get; set; } = new();
    }
    public class GenerateExamsView
    {
        public long SchoolId { get; set; }
        public long SubjectId { get; set; }
    }
    public class MerrorPdfData
    {
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public string MargeType { get; set; }
        public int PlaceNumber { get; set; }
        public string ExamTemplte { get; set; }
        public int HallNumber { get; set; }
        public string SchoolName { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public byte[] QrCode { get; set; }
    }
    public class PreparingFinalExamView
    {
        public long SchoolId { get; set; }
        public sbyte Exams { get; set; }
    }
    public class MerrorDataView
    {
        public long SchoolId { get; set; }
        public long SubjectId { get; set; }
        public string ClassName { get; set; }
    }
    public class StudentExamHallView
    {
        public long SchoolId { get; set; }
        public double Halls { get; set; }
        public List<string> ClassNames { get; set; } = new List<string>();
    }
    public class DeleteStudentExamView
    {
        public long SchoolId { get; set; }
        public List<string> ClassNames { get; set; } = new List<string>();
    }
    public class GetPlaceNumbersView
    {
        public long SchoolId { get; set; }
        public string ClassName { get; set; }
    }
    public class StudentsForAppliedExamData
    {
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public double Score { get; set; }
    }
    public class StudentsForAppliedExamView
    {
        public long ClassId { get; set; }
        public long SubjectId { get; set; }
    }
    public class SaveStudentsForAppliedExamView
    {
        public long SubjectId { get; set; }
        public long ClassId { get; set; }
        public bool IsSave { get; set; }
        public List<StudentsForAppliedExamData> Students { get; set; } = new();
    }
    public class StudentsPlaceNumbers
    {
        public string ReportName { get; set; }
        public List<HallsPlaceData> Halls { get; set; } = new();
    }
    public class HallsPlaceData
    {
        public string HallName { get; set; }
        public List<StudentsPlaceNumbersData> Students { get; set; } = new();
    }
    public class StudentsPlaceNumbersData
    {
        public string StudentName { get; set; }
        public int PlaceNumber { get; set; }
        public int PlaceInHall { get; set; }
    }
}
