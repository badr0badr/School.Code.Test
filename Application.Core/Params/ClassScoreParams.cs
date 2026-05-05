using System;
using System.Linq;

namespace Application.Core.Params
{
    public class ClassScoreParams : BaseParams
    {
        public long? WeekId { get; set; }
        public long? TeacherClassesId { get; set; }
        public int? Month { get; set; }
    }
}
