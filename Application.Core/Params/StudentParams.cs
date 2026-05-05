using System;
using System.Linq;

namespace Application.Core.Params
{
    public class StudentParams : BaseParams
    {
        public long? ClassId { get; set; }
        public long? Code { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
