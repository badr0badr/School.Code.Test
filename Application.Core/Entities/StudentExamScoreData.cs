﻿using Application.Core.Defults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class StudentExamScoreData : BaseEntity<long>
    {
        public int Month { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public long? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public long ClassId { get; set; }
        public Classes Class { get; set; }
        public bool IsSaved { get; set; }
        public bool IsSeen { get; set; }
        public bool IsAbsent { get; set; }
        public bool AbsentSaved { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double ExamResult { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Behavior { get; set; } = 0;
    }
}