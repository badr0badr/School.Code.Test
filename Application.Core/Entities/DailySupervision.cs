﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class DailySupervision : BaseEntity<long>
    {
        public long ClassId { get; set; }
        public Classes Class { get; set; }
        public long SchoolId { get; set; }
        public School School { get; set; }
        public long? Saturday { get; set; }
        public long? Sunday { get; set; }
        public long? Monday { get; set; }
        public long? Tuseday { get; set; }
        public long? Wednesday { get; set; }
        public long? Thursday { get; set; }
        public Teacher? TSaturday { get; set; }
        public Teacher? TSunday { get; set; }
        public Teacher? TMonday { get; set; }
        public Teacher? TTuseday { get; set; }
        public Teacher? TWednesday { get; set; }
        public Teacher? TThursday { get; set; }
    }
}