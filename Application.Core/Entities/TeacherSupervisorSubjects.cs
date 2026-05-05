﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class TeacherSupervisorSubjects : BaseEntity<long>
    {
        public long TeacherId { get; set; }
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public TeacherSupervisor Teacher { get; set; }
    }
}