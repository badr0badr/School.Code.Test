﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class TeacherSupervisor : BaseEntity<long>
    {
        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public long SchoolId { get; set; }
        public School School { get; set; }
        public string Stage { get; set; }
        public List<TeacherSupervisorSubjects> SubSubjects { get; set; } = new List<TeacherSupervisorSubjects>();
    }
}