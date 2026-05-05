﻿using Application.Core.Defults;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class ExamCodes : BaseEntity<long>
    {
        public long StudentId { get; set; }
        public Student Student { get; set; }
        public long SchoolId { get; set; }
        public School School { get; set; }
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int Code { get; set; }
        [Column(TypeName = AppDefults.DecimalColumnType)]
        public double Result { get; set; }

    }
}