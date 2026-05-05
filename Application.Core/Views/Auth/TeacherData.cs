using System;
using System.Linq;

namespace Application.Core.Views.Auth
{
    public class TeacherData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
        public long SchoolId { get; set; }
        public string? Role2 { get; set; }
        public string? Role2Name { get; set; }
        public bool HasSupervision { get; set; }
        public long MainSubject { get; set; }
        public bool HasQuiltyControl { get; set; }
        public bool HasControl { get; set; }
        public string ControlType { get; set; }
        public string Token { get; set; }
    }
    public class PerantData
    {
        public long Id { get; set; }
        public long Code { get; set; }
        public int StudentAbsent { get; set; } = 0;
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
