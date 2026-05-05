﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class ClassSubjectTermWork : BaseEntity<long>
    {
        public long SubjectId { get; set; }
        public string ClassName { get; set; }
        public long WeekId { get; set; }
        public Week Week { get; set; }
    }
}