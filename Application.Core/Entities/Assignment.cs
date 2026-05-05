﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class Assignment : BaseEntity<long>
    {
        public long TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public long SchoolId { get; set; }
        public School School { get; set; }
    }
}