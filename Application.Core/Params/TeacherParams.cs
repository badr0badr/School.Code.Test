using System;
using System.Linq;

namespace Application.Core.Params
{
    public class TeacherParams : BaseParams
    {
        public string? RoleId { get; set; }
        public long? SchoolId { get; set; }
        public long? MainSubjectId { get; set; }
        public bool? CanAccesControl { get; set; }
        public bool? HasQuiltyControl { get; set; }
    }
}
