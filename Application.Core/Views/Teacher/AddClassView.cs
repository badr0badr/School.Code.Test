using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Teacher
{
    public class AddClassView
    {
        public long SchoolId { get; set; } = 0;
        public string ClassId { get; set; }
        public string GradId { get; set; }
        public int ClassNumber { get; set; } = 0;
    }
    public class AddOrUpdateClassSubjectShareView
    {
        public long SubjectId { get; set; }
        public string ClassName { get; set; }
        public int ShareAmount { get; set; }
    }
    public class AddPAView
    {
        [Required(ErrorMessage = "ادخل الاسم")]
        public string Name { get; set; }
        public long SchoolId { get; set; }
        public string RoleId { get; set; }
    }
    public class AddSuperSubjectsView
    {
        public long TeacherId { get; set; }
        public long SubjectId { get; set; }
    }
    public class AddTeacherView
    {
        public long SchoolId { get; set; }
        [Required(ErrorMessage = "ادخل الاسم")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ادخل الوظيفه")]
        public string RoleId { get; set; }
        public string? Grade { get; set; }
        public long? TeacherTitleId { get; set; }
        public long? MainSubjectId { get; set; }
        public bool IsMainPower { get; set; }
    }
    public class ChangeSupervisorView
    {
        public long OldTeacherId { get; set; }
        public long NewTeacherId { get; set; }

    }
    public class ChangeQuiltyControlView
    {
        public long SchoolId { get; set; }
        public long TeacherId { get; set; }
    }
    public class CheckManagementView
    {
        public long SchoolId { get; set; }
        public long SubjectId { get; set; }
        public string Stage { get; set; }
    }
    public class ClassSubjectShareView
    {
        public long SubjectId { get; set; }
        public string SubjectName { get; set; }
        public List<ClassSubjectShareViewData> ShareData { get; set; } = new List<ClassSubjectShareViewData>();
    }
    public class ClassSubjectShareViewData
    {
        public string ClassName { get; set; }
        public int ShareAmount { get; set; }
    }
    public class SubjectClassShareView
    {
        public string ClassName { get; set; }
        public List<SubjectClassShareViewData> ShareData { get; set; } = new List<SubjectClassShareViewData>();
    }
    public class SubjectClassShareViewData
    {
        public string SubjectName { get; set; }
        public int ShareAmount { get; set; }
    }
    public class EditTeacherView
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? RoleId { get; set; }
        public long? TitleId { get; set; }
        public DateOnly? TitleDate { get; set; }
    }
    public class GetAssignTeacherView
    {
        public long Id { get; set; }
        public string TeacherName { get; set; }
        public string SubjectName { get; set; }
        public string ClassId { get; set; }
    }
    public class GetRolesView
    {
        public string Id { get; set; }
        public string Role { get; set; }
    }
    public class GetTeacherClassesView
    {
        public long Id { get; set; }
        public long SubjectId { get; set; }
        public string SubjectName { get; set; }
        public long ClassId { get; set; }
        public string ClassFullId { get; set; }
        public int Level { get; set; }
    }
    public class GetTeachersVisionView
    {
        public long SchoolId { get; set; }
        public string SuperType { get; set; }
    }
    public class ModifySupervisionView
    {
        public long TeacherId { get; set; }
        public string Day { get; set; }
        public long ClassId { get; set; }
        public string SuperType { get; set; }
    }
    public class DailySupervisionView
    {
        public string ClassId { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public string Day3 { get; set; }
        public string Day4 { get; set; }
        public string Day5 { get; set; }
        public string Day6 { get; set; }
    }
    public class GetSchoolView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<GetSchoolTeachers> Teachers { get; set; } = new();
    }
    public class GetSchoolTeachers
    {
        public long Id { get; set; }
        public string RoleId { get; set; }
        public string Name { get; set; }
    }
    public class GetTeachersView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Title { get; set; }
    }
    public class SchoolSharePowerView
    {
        public int TotalShare { get; set; } = 0;
        public List<SharePowerView> TShare { get; set; } = new List<SharePowerView>();
    }
    public class SharePowerView
    {
        public string TeacherName { get; set; }
        public string Title { get; set; }
        public DateOnly? TitleDate { get; set; }
        public int LegalShare { get; set; } = 0;
    }
    public class ClassSharePowerView
    {
        public string ReportName { get; set; }
        public int TotalShare { get; set; } = 0;
        public List<CSharePowerView> TShare { get; set; } = new List<CSharePowerView>();
    }
    public class CSharePowerView
    {
        public string ClassName { get; set; }
        public int LegalShare { get; set; } = 0;
        public int ClassNumber { get; set; } = 0;
    }
    public class SummaryShareView
    {
        public string Report { get; set; }
        public int TotalClassShare { get; set; } = 0;
        public int TotalTeacherShare { get; set; } = 0;
        public int Result { get; set; } = 0;
    }
    public class SummaryShareDetailsView
    {
        public string Subject { get; set; }
        public string Report { get; set; }
        public int TotalClassShare { get; set; } = 0;
        public int TotalTeacherShare { get; set; } = 0;
        public int Result { get; set; } = 0;
    }
    public class SubjectSummaryShareDetails
    {
        public string Report { get; set; }
        public int TotalClassShare { get; set; } = 0;
        public int TotalTeacherShare { get; set; } = 0;
        public int Result { get; set; } = 0;
        public List<SharePowerView> TeachersShare { get; set; } = new List<SharePowerView>();
        public List<CSharePowerView> ClassesShare { get; set; } = new List<CSharePowerView>();
    }
    public class StudentForClassView
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class SubjectMapView
    {
        public long SchoolId { get; set; } = 0;
        public long SubjectId { get; set; } = 0;
        public long? TeacherId { get; set; } = 0;
    }
    public class SubjectMapDetails
    {
        public string ReportName { get; set; }
        public string Report { get; set; }
        public int TotalLegalShare { get; set; } = 0;
        public int TotalCurrentShare { get; set; } = 0;
        public int TotalClasses { get; set; } = 0;
        public int TotalResult { get; set; } = 0;

        public int TotalMainLegalShare { get; set; } = 0;
        public int TotalMainCurrentShare { get; set; } = 0;
        public int TotalMainClasses { get; set; } = 0;
        public int TotalMainResult { get; set; } = 0;

        public int TotalSecandryLegalShare { get; set; } = 0;
        public int TotalSecandryCurrentShare { get; set; } = 0;
        public int TotalSecandryClasses { get; set; } = 0;
        public int TotalSecandryResult { get; set; } = 0;

        public int TotalFeeLegalShare { get; set; } = 0;
        public int TotalFeeCurrentShare { get; set; } = 0;
        public int TotalFeeClasses { get; set; } = 0;
        public int TotalFeeResult { get; set; } = 0;

        public List<SubjectMapDetailsView> TMainMap { get; set; } = new List<SubjectMapDetailsView>();
        public List<SubjectMapDetailsView> TSecandryMap { get; set; } = new List<SubjectMapDetailsView>();
        public List<SubjectMapDetailsView> TFeeMap { get; set; } = new List<SubjectMapDetailsView>();
    }

    public class SubjectMapDetailsView
    {
        public string TeacherName { get; set; }
        public string Title { get; set; }
        public int LegalShare { get; set; } = 0;
        public List<string> Classes { get; set; } = new List<string>();
        public int CurrentShare { get; set; } = 0;
        public int Result { get; set; } = 0;
    }
    public class TitelsShareView
    {
        public string Title { get; set; }
        public int LegalShare { get; set; } = 0;
        public int TotalTeachers { get; set; } = 0;
        public int TotalLegalShare { get; set; } = 0;
        public int TotalCurrentShare { get; set; } = 0;
    }
    public class TitelsShareViewData
    {
        public string ReportName { get; set; }
        public List<TitelsShareView> Data { get; set; } = new List<TitelsShareView>();
    }
    public class SubjectSummaryShareDetailsView
    {
        public long SchoolId { get; set; }
        public long SubjectId { get; set; }
    }
    public class TeacherTitleView
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int ShareAmount1_2 { get; set; }
        public int ShareAmount3 { get; set; }
        public int ShareAmount4 { get; set; }
    }
    public class TeacherToClassView
    {
        [Required(ErrorMessage = "ادخل المعلم")]
        public long TeacherId { get; set; }
        [Required(ErrorMessage = "ادخل الفصل")]
        public long ClassId { get; set; }
        [Required(ErrorMessage = "ادخل الماده")]
        public long SubjectId { get; set; }
    }
    public class AssignTeacherView
    {
        public long SubjectId { get; set; }
        public long SchoolId { get; set; }
    }

}
