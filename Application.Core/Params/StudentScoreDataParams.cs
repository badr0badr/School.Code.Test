using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Params
{
    public class StudentScoreDataParams
    {
        public long? StudentId { get; set; }
        public long? ClassId { get; set; }
        public long? WeekId { get; set; }
        public long? TeacherId { get; set; }
        public long? SubjectId { get; set; }
        public int? Month { get; set; }
        public bool? IsSaved { get; set; }
        public bool? IsPreAbsent { get; set; }

    }
}
