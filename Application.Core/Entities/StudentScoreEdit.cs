﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class StudentScoreEdit : BaseEntity<long>
    {
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public long? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public long StudentScoreDataId { get; set; }
        public StudentScoreData StudentScoreData { get; set; }
        public string Reason { get; set; }
        public bool IsEdited { get; set; }

    }
}