using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Params
{
    public class RoleParams
    {
        public string? Id { get; set; }
        public bool? SchoolSelection { get; set; }
        public bool? AdminSelection { get; set; }
        public bool? DirectorSelection { get; set; }
    }
}
