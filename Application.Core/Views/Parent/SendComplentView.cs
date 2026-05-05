using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Parent
{
    public class SendComplentView
    {
        public long StudentId { get; set; }
        public long SchoolId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
    public class StudentProfile
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public string ClassFullId { get; set; }
        public int? Rank { get; set; }
        public int WeekAbsent { get; set; }
        public int AllAbsent { get; set; }
        public List<KnowAbsentData> KnowAbsent { get; set; } = new();
        public List<AbsentData> Absent { get; set; } = new();
    }
    public class KnowAbsentData
    {
        public string From { get; set; }
        public string To { get; set; }
    }
    public class AbsentData
    {
        public string WeekName { get; set; }
        public int Absent { get; set; }
    }
    public class StudentComplentData
    {
        public long Id { get; set; }
        public string Body { get; set; }
        public string SendDate { get; set; }
        public string ConfirmedDate { get; set; }
        public bool IsConfirmed { get; set; }
    }
    public class StudentPunchmentsData
    {
        public long Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string ReportDate { get; set; }
        public bool IsViewed { get; set; }
        public bool IsConfirmed { get; set; }
    }

}
