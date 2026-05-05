using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Directorate
{
    public class AddAgencyUserView
    {
        public string Name { get; set; }
        public string RoleId { get; set; }
        public long SchoolId { get; set; }
    }
    public class AddAgencyView
    {
        public string AgencyName { get; set; }
        public string AgencyMangerName { get; set; }
    }
}
