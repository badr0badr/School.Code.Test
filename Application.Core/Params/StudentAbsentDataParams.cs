using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Params
{
    public class StudentAbsentDataParams : BaseParams
    {
        public long? StudentId { get; set; }
        public long? WeekId { get; set; }
        public long? TeacherId { get; set; }
        public long? ClassId { get; set; }
        public long? SubjectId { get; set; }
        public bool? IsAbsent { get; set; }
        public DateOnly? Date { get; set; }
        public int? ClassNumber { get; set; }
    }
}
