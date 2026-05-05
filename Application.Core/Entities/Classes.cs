﻿using System;
using System.Linq;

namespace Application.Core.Entities
{
    public class Classes : BaseEntity<long>
    {
        public string Name { get; set; }
        public string Grade { get; set; }
        public int Class { get; set; }
        public int ClassNumber { get; set; }
        public string FullId { get; set; }
        public int ClassType { get; set; }
        public long SchoolId { get; set; }
        public School School { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}