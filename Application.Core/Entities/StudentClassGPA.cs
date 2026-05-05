﻿using Application.Core.Defults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class StudentClassGPA : BaseEntity<long>
    {
        public long ClassId { get; set; }
        public Classes Class { get; set; }
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public long? StudentId { get; set; }
        public Student Student { get; set; }
        public long WeeKId { get; set; }
        public Week Week { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Review { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Behavior { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Homework { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Activity { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Missions { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Oral { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Classwork { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double TotalRate { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double FirstExam { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double SecondExam { get; set; } = 0;
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double ThirdExam { get; set; } = 0;
    }
}