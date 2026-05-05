using Application.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Views.Auth
{
    public class GetProfileView
    {
        public long Id { get; set; }
        public long SchoolId { get; set; }
        public long MangerId { get; set; } = 0;
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string SchoolName { get; set; }
        public string MangerName { get; set; } = "";
        public List<string> Classes { get; set; } = new List<string>();
        public List<string> Managed { get; set; } = new List<string>();
        public List<string> Subjects { get; set; } = new List<string>();
    }
}
