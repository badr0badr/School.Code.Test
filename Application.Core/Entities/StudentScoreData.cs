﻿using Application.Core.Defults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class StudentScoreData : BaseEntity<long>
    {
        public long WeekId { get; set; }
        public Week Week { get; set; }
        public long? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public long ClassId { get; set; }
        public Classes Class { get; set; }
        public bool IsSaved { get; set; }
        public bool IsPreAbsent { get; set; }
        public bool IsSeen { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Review { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Behavior { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Homework { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Activity { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Missions { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Oral { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Classwork { get; set; }
    }
}