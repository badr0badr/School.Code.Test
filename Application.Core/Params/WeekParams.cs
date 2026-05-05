using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Params
{
    public class WeekParams : BaseParams
    {
        public bool? IsActive { get; set; }
        public long? Id { get; set; }
        public int? Month { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? Index { get; set; }
        public DateOnly? Date { get; set; }
    }
}
