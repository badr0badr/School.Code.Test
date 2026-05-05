using System;
using System.Linq;

namespace Application.Core.Params
{
    public class BaseParams
    {
        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }
    }
    public class AdministrationsParams : BaseParams
    {
        public string? Name { get; set; }
    }
    public class AdministrationsVacationParams : BaseParams
    {
        public long? AdministrationId { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public DateOnly? Date { get; set; }
        public string? Reason { get; set; }
    }
    public class AssignmentParams
    {
        public long? TeacherId { get; set; }
        public long? SchoolId { get; set; }
    }
    public class ClassesParams : BaseParams
    {
        public string? FullId { get; set; }
        public string? Name { get; set; }
        public string? Grade { get; set; }
        public string? Class { get; set; }
        public long? SchoolId { get; set; }
        public int? ClassNumber { get; set; }
    }
    public class ClassSubjectShareParams
    {
        public long? SubjectId { get; set; }
        public string? ClassName { get; set; }
        public int? ShareAmount { get; set; }
    }
    public class ClassSubjectTermWorklParams : BaseParams
    {
        public long? SubjectId { get; set; }
        public string? ClassName { get; set; }
        public long? WeekId { get; set; }
        public int? Month { get; set; }
    }
    public class ExamCodesParams
    {
        public long? StudentId { get; set; }
        public long? SubjectId { get; set; }
        public int? Code { get; set; }
    }
    public class SchoolParams : BaseParams
    {
        public string? Name { get; set; }
        public long? SchoolMangerId { get; set; }
        public long? AdministrationId { get; set; }
    }
    public class SendStudentToSchoolParams
    {
        public long? SchoolSenderId { get; set; }
        public long? SchoolReceiverId { get; set; }
        public long? StudentId { get; set; }
        public bool? CanBeSend { get; set; }
    }
    public class StudentClassGPAParams
    {
        public long? ClassId { get; set; }
        public long? StudentId { get; set; }
        public long? WeeKId { get; set; }
    }
    public class StudentExamScoreDataParams
    {
        public long? StudentId { get; set; }
        public long? ClassId { get; set; }
        public int? Month { get; set; }
        public long? TeacherId { get; set; }
        public long? SubjectId { get; set; }
        public bool? IsSaved { get; set; }
    }







}
