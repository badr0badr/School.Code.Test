﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class StudentAbsentData : BaseEntity<long>
    {
        public long WeekId { get; set; }
        public Week Week { get; set; }
        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public long ClassId { get; set; }
        public Classes Class { get; set; }
        public long? SubjectId { get; set; }
        public Subject Subject { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public bool IsAbsent { get; set; }
        public string? Reason { get; set; }
        public DateOnly Date { get; set; }
        public int ClassNumber { get; set; }
    }
}