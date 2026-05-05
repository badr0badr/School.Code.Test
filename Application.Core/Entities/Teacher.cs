﻿using System;
using System.Linq;

namespace Application.Core.Entities
{
    public class Teacher : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string? Color { get; set; }
        public string? Grade { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public string? RoleId2 { get; set; }
        public Role? Role2 { get; set; }
        public long SchoolId { get; set; }
        public School School { get; set; }
        public DateOnly? TeacherTitleDate { get; set; }
        public long? TeacherTitleId { get; set; }
        public TeacherTitle TeacherTitle { get; set; }
        public long? MainSubjectId { get; set; }
        public Subject? MainSubject { get; set; }
        public bool CanAccesControl { get; set; }
        public bool HasQuiltyControl { get; set; }
        public string? ControlType { get; set; }
        public bool IsMainPower { get; set; }
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<TeacherClasses> Classes { get; set; } = new List<TeacherClasses>();
    }
}