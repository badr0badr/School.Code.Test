using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Dashboard
{
    public class AvargeReportDataRecords
    {
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public double Review { get; set; }
        public double Behavior { get; set; }
        public double Homework { get; set; }
        public double Activity { get; set; }
        public double Missions { get; set; }
        public double Oral { get; set; }
        public double Classwork { get; set; }
        public double Total { get; set; }
        public double Exam { get; set; }
        public double ExamBehavior { get; set; }
        public double AllTotal { get; set; }
    }
    public class FinishClassDashDataView
    {
        public List<string> Weeks { get; set; } = new List<string>();
        public List<FinishClassDashViewData> Data { get; set; } = new List<FinishClassDashViewData>();
    }
    public class FinishClassDashViewData
    {
        public long ClassId { get; set; }
        public string ClassFullId { get; set; }
        public string TeacherName { get; set; }
        public List<FinishClassDashViewDataWeeks> WeekChecked { get; set; } = new List<FinishClassDashViewDataWeeks>();
    }
    public class FinishClassDashViewDataWeeks
    {
        public long WeekId { get; set; }
        public bool Check { get; set; }
    }
    public class FinishClassDashExamDataView
    {
        public sbyte Level { get; set; }
        public List<string> Subjects { get; set; } = new();
        public List<FinishClassDashExamDataSubjects> Classes { get; set; } = new();
    }
    public class FinishClassDashExamDataSubjects
    {
        public string FullId { get; set; }
        public long ClassId { get; set; }
        public List<FinishClassDashExamDataSubjectsChecks> Checks { get; set; } = new();
    }
    public class FinishClassDashExamDataSubjectsChecks
    {
        public long SubjectId { get; set; }
        public sbyte Cheak { get; set; }
    }
    public class FinishClassDashView
    {
        public long SubjectId { get; set; }
        public long SchoolId { get; set; }
        public int Month { get; set; }
    }
    public class FinishClassDashExamView
    {
        public long SchoolId { get; set; }
        public string ClassName { get; set; }
        public int Month { get; set; }
    }
    public class FinishTeacherClassDashView
    {
        public long TeacherId { get; set; }
        public long SubjectId { get; set; }
        public int Month { get; set; }
    }
    public class Get5BehaviorView
    {
        public long Month { get; set; }
        public long ClassId { get; set; }
        public string ClassFullId { get; set; }
        public List<Student5BehaviorView> Students { get; set; } = new List<Student5BehaviorView>();
    }
    public class Student5BehaviorView
    {
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public List<StudentDayDetails> studentDays { get; set; } = new List<StudentDayDetails>();
    }
    public class StudentDayDetails
    {
        public long Day { get; set; }
        public string Name { get; set; }
        public int IsExist { get; set; }
        public string Reason { get; set; }
    }
    public class BehaviorView
    {
        public long Month { get; set; }
        public int Year { get; set; }
        public long ClassId { get; set; }
    }
    public class GetAllAverageMonthReport
    {
        public string ReportName { get; set; }
        public List<AllAverageMonthReport> AverageReport { get; set; } = new List<AllAverageMonthReport>();
    }
    public class AllAverageMonthReport
    {
        public string StudentName { get; set; }
        public string ClassFullId { get; set; }
        public List<SubjectAverageMonthReport> Report { get; set; } = new List<SubjectAverageMonthReport>();
    }
    public class SubjectAverageMonthReport
    {
        public string SubjectName { get; set; }
        public double Review { get; set; } = 0;
        public double Behavior { get; set; } = 0;
        public double Homework { get; set; } = 0;
        public double Total { get; set; } = 0;
        public double Exam { get; set; } = 0;
    }
    public class GetAllAverageMonthReportView
    {
        public long SchoolId { get; set; }
        public int Month { get; set; }
    }
    public class GetAverageReportDataView
    {
        public string ReportName { get; set; }
        public int Level { get; set; }
        public List<AverageReportData> AverageReport { get; set; } = new List<AverageReportData>();
    }
    public class AverageReportData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Review { get; set; }
        public double Behavior { get; set; }
        public double Homework { get; set; }
        public double Activity { get; set; }
        public double Missions { get; set; }
        public double Oral { get; set; }
        public double Classwork { get; set; }
        public double Total { get; set; }
        public double Exam { get; set; }
        public double ExamBehavior { get; set; }
        public double AllTotal { get; set; }
    }
    public class GetVacationView
    {
        public long Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set; }
    }
    public class AddVacationView
    {
        public long SchoolId { get; set; } = 0;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set; }
    }
    public class GetWeeklyReportView
    {
        public long UserId { get; set; }
        public long? TeacherClassesId { get; set; }
        public long? SubjectId { get; set; }
        public long? ClassId { get; set; }
        public long WeekId { get; set; }
    }
    public class MonthlyReportDataView
    {
        public string ReportName { get; set; }
        public int Level { get; set; }
        public List<MonthlyReportData> MonthlyReport { get; set; } = new List<MonthlyReportData>();

    }
    public class MonthlyReportData
    {
        public string StudentName { get; set; }
        public double Exam { get; set; }
        public double ExamBehavior { get; set; }
        public List<MonthlyReportWeek> Weeks { get; set; } = new List<MonthlyReportWeek>();
    }
    public class MonthlyReportWeek
    {
        public string WeekName { get; set; }
        public double Review { get; set; }
        public double Behavior { get; set; }
        public double Homework { get; set; }
        public double Activity { get; set; }
        public double Missions { get; set; }
        public double Oral { get; set; }
        public double Classwork { get; set; }
        public double Total { get; set; }
    }
    public class StudentMonthlyReportDataView
    {
        public string ReportName { get; set; }
        public List<StudentMonthlyReport> StudentMonthlyReport { get; set; } = new List<StudentMonthlyReport>();
    }
    public class StudentMonthlyReport
    {
        public string ClassName { get; set; }
        public long ClassId { get; set; }
        public string StudentName { get; set; }
        public string MonthName { get; set; }
        public double FullTotal { get; set; } = 0;
        public double Absent { get; set; } = 0;
        public List<StudentMonthlyReportSubjectRecord> SubjectData { get; set; } = new List<StudentMonthlyReportSubjectRecord>();
    }
    public class StudentMonthlyReportSubjectRecord
    {
        public string SubjectName { get; set; }
        public string Status { get; set; }
        public double Review { get; set; } = 0;
        public double Homework { get; set; } = 0;
        public double Behavior { get; set; } = 0;
        public double Total { get; set; } = 0;
        public double Exam { get; set; } = 0;
        public double AllTotal { get; set; } = 0;
    }
    public class GetStudentMonthlyReportView
    {
        public long ClassId { get; set; }
        public long UserId { get; set; }
        public int Month { get; set; }
    }
    public class TeachersView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<TeachersViewSubjects> Subjects { get; set; } = new List<TeachersViewSubjects>();
    }
    public class TeachersViewSubjects
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class WeeklyReportData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Review { get; set; }
        public double Behavior { get; set; }
        public double Homework { get; set; }
        public double Activity { get; set; }
        public double Missions { get; set; }
        public double Oral { get; set; }
        public double Classwork { get; set; }
        public double Total { get; set; }
    }
    public class WeeklyReportDataView
    {
        public string ReportName { get; set; }
        public int Level { get; set; }
        public List<WeeklyReportData> weeklyReport { get; set; } = new List<WeeklyReportData>();
    }

}
