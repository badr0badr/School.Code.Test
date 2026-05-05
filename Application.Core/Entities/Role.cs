﻿using System;
using System.Linq;

namespace Application.Core.Entities
{
    public class Role : BaseEntity<string>
    {
        public string RoleArabic { get; set; }
        public int Index { get; set; }
        public bool SchoolSelection { get; set; }
        public bool AdminSelection { get; set; }
        public bool DirectorSelection { get; set; }
    }
}