﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class StudentAbsentCases : BaseEntity<long>
    {
        public long? StudentId { get; set; }
        public Student Student { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Reason { get; set; }
        public bool IsPatient { get; set; }
    }
}