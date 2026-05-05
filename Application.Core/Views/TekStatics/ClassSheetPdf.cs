using Application.Core.Views.Score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.TekStatics
{
    public class ClassSheetPdf
    {
        public string ScoreSheet { get; set; }
        public string AbsentSheet { get; set; }
    }
    public class ClassSheetPdfView
    {
        public long ClassId { get; set; }
        public int Month { get; set; }
    }
    public class MonitorGradesDetails
    {
        public List<string> Subjects { get; set; } = new List<string>();
        public List<MonitorGradesDetailsClass> Classes { get; set; } = new List<MonitorGradesDetailsClass>();
    }
    public class MonitorGradesDetailsClass
    {
        public long ClassId { get; set; }
        public string ClassName { get; set; }
        public List<MonitorGradesDetailsSubject> Subjects { get; set; } = new List<MonitorGradesDetailsSubject>();
    }
    public class MonitorGradesDetailsSubject
    {
        public long SubjectId { get; set; }
        public sbyte Status { get; set; } = 0;
    }
    public class MonitorGradesView
    {
        public long SchoolId { get; set; }
        public long WeekId { get; set; }
        public string Grade { get; set; }
    }
    public class MonitorGradesDataDetails
    {
        public string TeacherName { get; set; }
        public string ClassFullId { get; set; }
        public string SubjectName { get; set; }
        public long TeacherId { get; set; }
        public long ClassId { get; set; }
        public long SubjectId { get; set; }
        public sbyte Level { get; set; }
        public List<GetStudentForClassScore> Scores { get; set; } = new List<GetStudentForClassScore>();
    }

    public class MonitotExamGradesDataDetails
    {
        public string TeacherName { get; set; }
        public long TeacherId { get; set; }
        public string ClassFullId { get; set; }
        public long ClassId { get; set; }
        public string SubjectName { get; set; }
        public long SubjectId { get; set; }
        public List<StudentExamScoreDData> Scores { get; set; } = new();
    }
    public class StudentExamScoreDData
    {
        public string Name { get; set; }
        public double Exam { get; set; } = 0;
        public bool PreAbsent { get; set; } = false;

    }
    public class StudentDataAvarge
    {
        public long StudentId { get; set; }
        public double Review { get; set; } = 0;
        public double Behavior { get; set; } = 0;
        public double Homework { get; set; } = 0;
        public double Activity { get; set; } = 0;
        public double Missions { get; set; } = 0;
        public double Oral { get; set; } = 0;
        public double Classwork { get; set; } = 0;
        public double TotalRate { get; set; } = 0;
        public List<StudentDataAvargeScores> Scores { get; set; } = new();
    }
    public class StudentDataAvargeScores
    {
        public double Review { get; set; } = 0;
        public double Behavior { get; set; } = 0;
        public double Homework { get; set; } = 0;
        public double Activity { get; set; } = 0;
        public double Missions { get; set; } = 0;
        public double Oral { get; set; } = 0;
        public double Classwork { get; set; } = 0;
    }
    public class StudentForExamScoreView
    {
        public long SubjectId { get; set; }
        public long ClassId { get; set; }
        public int Month { get; set; }
    }
    public class StudentForExamGradeView
    {
        public long SchoolId { get; set; }
        public int Month { get; set; }
    }

}
