using Application.Core.Views.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Reports
{
    public class AvargeForClassData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public sbyte Level { get; set; }
        public double Review { get; set; } = 0;
        public double Behavior { get; set; } = 0;
        public double Homework { get; set; } = 0;
        public double Activity { get; set; } = 0;
        public double Missions { get; set; } = 0;
        public double Oral { get; set; } = 0;
        public double Classwork { get; set; } = 0;
    }
    public class CertificateReportData
    {
        public List<string> SubjectName { get; set; } = new();
        public List<CertificateReportClassesData> ClassesData { get; set; } = new();
    }
    public class CertificateReportClassesData
    {
        public string ClassName { get; set; }
        public List<double> TotalPersent { get; set; } = new();
        public List<ReportClassData> ClassData { get; set; } = new();
    }
    public class ReportClassData
    {
        public string ClassFullId { get; set; }
        public List<double> TotalPersent { get; set; } = new();
    }
    public class CertificateReportView
    {
        public long SchoolId { get; set; }
        public int Month { get; set; }
    }
    public class ClassDigramData
    {
        public sbyte Level { get; set; }
        public long ClassId { get; set; }
        public string ClassFullId { get; set; }
        public List<DigramData> Scores { get; set; } = new();
        public List<StudenDigramDatat> Students { get; set; } = new();
    }
    public class StudenDigramDatat
    {
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public List<DigramData> Scores { get; set; } = new();
        public double Progress { get; set; }
    }
    public class DigramData
    {
        public long WeekId { get; set; }
        public string WeekName { get; set; }
        public double Score { get; set; }
    }
    public class ClassDigramView
    {
        public long ClassId { get; set; }
        public long SubjectId { get; set; }
    }
    public class PreparingExamModel
    {
        public string ReportName { get; set; }
        public List<string> ModelNames { get; set; } = new();
        public List<PreparingExamModelClasses> Classes { get; set; } = new();

    }
    public class PreparingExamModelClasses
    {
        public string ClassFullId { get; set; }
        public int TotalStudents { get; set; }
        public int Marge { get; set; }
        public int ViewMarge { get; set; }
        public List<short> Models { get; set; } = new();
    }
    public class ExamTempltesModelData
    {
        public string ClassName { get; set; }
        public List<IdNumberNameView> MainModels { get; set; } = new();
        public List<IdNumberNameView> MargeModels { get; set; } = new();
        public List<IdNumberNameView> VisionModels { get; set; } = new();

    }
    public class PreparingExamView
    {
        public long SchoolId { get; set; }
        public sbyte Exams { get; set; }
        public string ClassName { get; set; }
    }
    public class StudentCertificateData
    {
        public sbyte Level { get; set; }
        public List<StudentCertificateSubjectData> Subject { get; set; } = new();
    }
    public class StudentCertificateSubjectData
    {
        public string SubjectName { get; set; }
        public string SubjectType { get; set; }
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
    public class StudentCertificateView
    {
        public long StudentId { get; set; }
        public long SubjectId { get; set; }
        public int Month { get; set; }
    }
    public class CertificateSettingView
    {
        public string ClassName { get; set; }
        public long SchoolId { get; set; }
        public long WeekId { get; set; }
        public int Month { get; set; }
    }
    public class StudentMonthlyCertificateSettingData
    {
        public long Id { get; set; }
        public string ClassName { get; set; }
        public long WeekId { get; set; }
        public string WeekName { get; set; }
        public string Month { get; set; }
        public bool StartExam { get; set; }
        public bool StartCertificates { get; set; }
    }
    public class TotalAvrageViewClassData
    {
        public List<string> SubjectNames { get; set; } = new();
        public List<TotalAvrageViewClassDetails> Classes { get; set; } = new();
    }
    public class TotalAvrageViewClassDetails
    {
        public long ClassId { get; set; }
        public string ClassFullId { get; set; }
        public List<TotalAvrageView> Subjects { get; set; } = new();
    }
    public class TotalAvrageView
    {
        public long SubjectId { get; set; }
        public double Persent { get; set; }
        public string WeekName { get; set; }
    }
    public class AvrageView
    {
        public long SchoolId { get; set; }
        public string ClassName { get; set; }
    }
    public class ClassAvrageView
    {
        public long ClassId { get; set; }
        public long SubjectId { get; set; }
    }
    public class TotalClassAvrage
    {
        public sbyte Level { get; set; }
        public List<TotalClassAvrageDetails> Students { get; set; } = new();
    }
    public class TotalClassAvrageDetails
    {
        public string StudentName { get; set; }
        public double Review { get; set; }
        public double Behavior { get; set; }
        public double Homework { get; set; }
        public double Activity { get; set; }
        public double Missions { get; set; }
        public double Oral { get; set; }
        public double Classwork { get; set; }
        public double Total { get; set; }
    }
    public class TotalStudentAvrage
    {
        public sbyte Level { get; set; }
        public string ClassFullId { get; set; }
        public List<TotalSubjectsAvrageDetails> Subjects { get; set; } = new();
    }
    public class TotalSubjectsAvrageDetails
    {
        public string SubjectsName { get; set; }
        public string WeekName { get; set; }
        public double Review { get; set; }
        public double Behavior { get; set; }
        public double Homework { get; set; }
        public double Activity { get; set; }
        public double Missions { get; set; }
        public double Oral { get; set; }
        public double Classwork { get; set; }
        public double Total { get; set; }
        public double Exam1 { get; set; }
        public double Exam2 { get; set; }
    }
    public class ClassSubjectsDashBord
    {
        public string ClassName { get; set; }
        public List<SubjectDashBord> Subjects { get; set; }
    }
    public class SubjectDashBord
    {
        public string SubjectName { get; set; }
        public sbyte Stage1 { get; set; }
        public sbyte Stage2 { get; set; }
        public sbyte Stage3 { get; set; }
        public sbyte Stage4 { get; set; }
        public sbyte Stage5 { get; set; }
    }
    public class FinalAvargeView
    {
        public string ClassName { get; set; }
        public long SchoolId { get; set; }
    }
    public class HandlingExamPapersToRateView
    {
        public string ClassName { get; set; }
        public long SchoolId { get; set; }
        public long SubjectId { get; set; }
    }
    public class HandlingExamPapersToRateData
    {
        public string ControlManager { get; set; }
        public string SchoolManager { get; set; }
        public string SubjectName { get; set; }
        public string SchoolName { get; set; }
        public string Day { get; set; }
        public string Date { get; set; }
        public string ClassName { get; set; }
        public string SubjectManager { get; set; }
        public List<IdNumberNameView> Models { get; set; } = new();
    }

}
