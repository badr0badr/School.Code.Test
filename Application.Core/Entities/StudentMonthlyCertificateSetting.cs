﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class StudentMonthlyCertificateSetting : BaseEntity<long>
    {
        public string ClassName { get; set; }
        public long SchoolId { get; set; }
        public School School { get; set; }
        public long WeekId { get; set; }
        public Week Week { get; set; }
        public int Month { get; set; }
        public bool StartExam { get; set; }
        public bool StartCertificates { get; set; }
    }
}