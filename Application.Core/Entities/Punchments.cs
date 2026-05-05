﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class Punchments : BaseEntity<long>
    {
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public long SchoolId { get; set; }
        public School School { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public bool IsSeen { get; set; }
        public bool IsConfirmed { get; set; }
        public DateOnly SendDate { get; set; }
        public DateOnly? ConfirmedDate { get; set; }
        public DateOnly? SeenDate { get; set; }
    }
}