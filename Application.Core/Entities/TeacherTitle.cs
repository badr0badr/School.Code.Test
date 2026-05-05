﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class TeacherTitle : BaseEntity<long>
    {
        public string Title { get; set; }
        public int Index { get; set; }
        public int ShareAmount1_2 { get; set; }
        public int ShareAmount3 { get; set; }
        public int ShareAmount4 { get; set; }
    }
}