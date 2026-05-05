using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Adminstration
{
    public class AssignTeacherToSchoolView
    {
        public long TeacherId { get; set; }
        public long SchoolId { get; set; }
    }
    public class TransferTeacherView
    {
        public long TeacherId { get; set; }
        public long NewSchoolId { get; set; }
        public long OldSchoolId { get; set; }
    }
}
