﻿using System;
using System.Linq;

namespace Application.Core.Entities
{
    public class Subject : BaseEntity<long>
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public string Status { get; set; }
        public bool HasAppliedExam { get; set; }
        public List<ClassSubjectShare> ClassSubjectShares { get; set; } = new List<ClassSubjectShare>();
    }
}