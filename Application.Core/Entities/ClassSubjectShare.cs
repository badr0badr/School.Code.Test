﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class ClassSubjectShare : BaseEntity<long>
    {
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string ClassName { get; set; }
        public int ShareAmount { get; set; }
    }
}