﻿using System;
using System.Linq;

namespace Application.Core.Entities
{
    public class TeacherClasses : BaseEntity<long>
    {
        public long TeacherId { get; set; }
        public long SubjectId { get; set; }
        public long ClassId { get; set; }
        public Classes Class { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
    }
}