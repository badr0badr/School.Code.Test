using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Params
{
    public class SubjectParams : BaseParams
    {
        public string? Status { get; set; }
        public string? NotStatus { get; set; }
    }
}
