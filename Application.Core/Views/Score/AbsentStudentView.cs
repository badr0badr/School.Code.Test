using Application.Core.Views.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Score
{
    public class AbsentStudentView
    {
        public long Id { get; set; }
        public string StudentName { get; set; }
        public int TotalAbsent { get; set; }
        public int Level { get; set; }
        public bool IsExist { get; set; }
    }
    public class AddEditScoresView
    {
        public long StudentId { get; set; } = 0;
        public long TeacherId { get; set; }
        public long ScoreId { get; set; } = 0;
        public string Reason { get; set; }
    }
    public class AddScoresView
    {
        public int Month { get; set; }
        public bool? IsSaved { get; set; }
        public long WeekId { get; set; } = 0;
        public long TeacherClassesId { get; set; }
        public List<addStudentScore> StudentScores { get; set; } = new List<addStudentScore>();
    }
    public class ScoreForPreAbsentData
    {
        public long ScoreId { get; set; }
        public string StudentName { get; set; }
        public string ClassFullId { get; set; }
        public string WeekName { get; set; }
        public int Weekindex { get; set; }
        public string SubjectName { get; set; }
        public double level { get; set; } = 0;
        public double Review { get; set; } = 0;
        public double Behavior { get; set; } = 0;
        public double Homework { get; set; } = 0;
        public double Activity { get; set; } = 0;
        public double Missions { get; set; } = 0;
        public double Oral { get; set; } = 0;
        public double Classwork { get; set; } = 0;
    }
    public class ScoreForPreAbsentView
    {
        public long SchoolId { get; set; }
        public long TeacherId { get; set; }
    }
    public class addStudentScore
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Review { get; set; } = 0;
        public double Behavior { get; set; } = 0;
        public double Homework { get; set; } = 0;
        public double Activity { get; set; } = 0;
        public double Missions { get; set; } = 0;
        public double Oral { get; set; } = 0;
        public double Classwork { get; set; } = 0;
        public double Absent { get; set; } = 0;
        public bool PreAbsent { get; set; } = false;
    }
    public class AllAbsentView
    {
        public long Id { get; set; }
        public string TeacherName { get; set; }
        public string Absent { get; set; }
        public long ClassId { get; set; }
        public string ClassFullId { get; set; }
    }
    public class GetAllAbsentDataView
    {
        public string LessonNumber { get; set; }
        public List<AllAbsentView> Data { get; set; } = new List<AllAbsentView>();
    }
    public class MainAllAbsentView
    {
        public List<TopTable> H { get; set; } = new List<TopTable>();
        public List<TopTable> G { get; set; } = new List<TopTable>();
        public List<TopTable> F { get; set; } = new List<TopTable>();
        public List<AllAbsentView> Data { get; set; } = new List<AllAbsentView>();
    }
    public class TopTable
    {
        public int TotalStudent { get; set; } = 0;
        public int TotalAbsent { get; set; } = 0;
        public int TotalExist { get; set; } = 0;
        public string Present { get; set; } = "0%";
        public string Name { get; set; }
    }
    public class StudentAbsentDataExist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsExist { get; set; }
    }
    public class ClassExamData
    {
        public List<addStudentScore> Students { get; set; } = new();
        public string MonthName { get; set; }
        public int Month { get; set; }
    }
    public class DeleteScoreView
    {
        public long TeacherId { get; set; }
        public long ClassId { get; set; }
        public long WeekId { get; set; } = 0;
        public long SubjectId { get; set; } = 0;
    }
    public class EditStudentScoreView
    {
        public long StudentScoreDataId { get; set; } = 0;
        public bool? IsPreAbsent { get; set; }
        public double Review { get; set; }
        public double Behavior { get; set; }
        public double Homework { get; set; }
        public double Activity { get; set; }
        public double Missions { get; set; }
        public double Oral { get; set; }
        public double Classwork { get; set; }
    }
    public class GetAllAbsentViewDataView
    {
        public long SchoolId { get; set; }
        public DateOnly? Date { get; set; }
        public long? TeacherId { get; set; }
    }
    public class GetStudentForClassScore
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Review { get; set; } = 0;
        public double Behavior { get; set; } = 0;
        public double Homework { get; set; } = 0;
        public double Activity { get; set; } = 0;
        public double Missions { get; set; } = 0;
        public double Oral { get; set; } = 0;
        public double Classwork { get; set; } = 0;
        public double Absent { get; set; } = 0;
        public bool PreAbsent { get; set; } = false;

    }
    public class StudentForClassScoreView
    {
        public long ClassId { get; set; }
        public long WeekId { get; set; }
        public long SubjectId { get; set; }

    }
    public class IsAbsentClassExistView
    {
        public long? TeacherClassesId { get; set; }
        public long? ClassId { get; set; }
        public long? TeacherId { get; set; }
        public int ClassNumber { get; set; }
        public bool IsTwoPart { get; set; }
    }
    public class SaveAbsentStudentsView
    {
        public long? TeacherClassesId { get; set; }
        public long? ClassId { get; set; }
        public long? TeacherId { get; set; }
        public int ClassNumber { get; set; }
        public bool IsTwoPart { get; set; }
        public List<SaveAbsentStudentsViewData> Students { get; set; } = new();
    }
    public class SaveAbsentStudentsViewData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsExist { get; set; }

    }
    public class SaveAbsentStudentsExamView
    {
        public long ClassId { get; set; }
        public long TeacherId { get; set; }
        public long SubjectId { get; set; }
        public int Month { get; set; }
        public List<SaveAbsentStudentsExamViewData> Students { get; set; } = new();
    }
    public class SaveAbsentStudentsExamViewData
    {
        public long Id { get; set; }
        public bool IsExist { get; set; }
    }
    public class ExamAbsentData
    {
        public int Month { get; set; }
        public string MonthName { get; set; }
        public List<IdNumberNameView> Teachers { get; set; } = new();
        public List<ExamAbsentDataClass> Classes { get; set; } = new();

    }
    public class ExamAbsentDataClass
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<IdNumberNameView> Subjects { get; set; } = new();
    }
    public class ExamAbsentDataView
    {
        public long SchoolId { get; set; }
        public string ClassName { get; set; }
    }
}
