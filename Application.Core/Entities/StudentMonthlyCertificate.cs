﻿using Application.Core.Defults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class StudentMonthlyCertificate : BaseEntity<long>
    {
        public long ClassId { get; set; }
        public Classes Class { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int Month { get; set; }
        public bool UnderUpdate { get; set; }
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
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Total { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Exam { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double ExamBehavior { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double AllTotal { get; set; }
    }
}