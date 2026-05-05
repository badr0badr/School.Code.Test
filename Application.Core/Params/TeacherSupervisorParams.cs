using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Params
{
    public class TeacherSupervisorParams : BaseParams
    {
        public long? TeacherId { get; set; }
        public long? SubjectId { get; set; }
        public long? SchoolId { get; set; }
        public string? Stage { get; set; }

    }
}
