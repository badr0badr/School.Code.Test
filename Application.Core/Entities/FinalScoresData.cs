﻿using Application.Core.Defults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class FinalScoresData : BaseEntity<long>
    {
        public long StudentId { get; set; }
        public long SubjectId { get; set; }
        public long ClassId { get; set; }
        public Student Student { get; set; }
        public Classes Class { get; set; }
        public Subject Subject { get; set; }
        public int Term { get; set; }
        public bool IsPassed { get; set; }
        public string EducationalYear { get; set; }



        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Review { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Behavior { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Homework { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Exam { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double FinalExam { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double AppliedExam { get; set; }
        public bool IsAppliedSaved { get; set; }
    }
}