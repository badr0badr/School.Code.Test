using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Params
{
    public class ClassExamScoreParams : BaseParams
    {
        public int? Month { get; set; }
        public long? TeacherClassesId { get; set; }
    }
}
