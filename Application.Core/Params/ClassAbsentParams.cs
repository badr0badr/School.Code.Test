using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Params
{
    public class ClassAbsentParams : BaseParams
    {
        public long? TeacherClassesId { get; set; }
        public long? TeacherId { get; set; }
        public long? ClassId { get; set; }
        public DateOnly? Date { get; set; }
        public int? ClassNumber { get; set; }
    }
}
