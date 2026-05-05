using System;
using System.Linq;

namespace Application.Core.Params
{
    public class TeacherClassesParams : BaseParams
    {
        public long? TeacherId { get; set; }
        public long? ClassId { get; set; }
        public long? SubjectId { get; set; }
        public long? SchoolId { get; set; }
        public string? ClassName { get; set; }
    }
}
