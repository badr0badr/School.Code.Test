﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Entities
{
    public class School : BaseEntity<long>
    {
        public string Name { get; set; }
        public long? SchoolMangerId { get; set; }
        public long AdministrationId { get; set; }
        public int index { get; set; }
        public Administrations Administration { get; set; }
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public List<Classes> Classes { get; set; } = new List<Classes>();
        public List<SchoolVacation> Vacations { get; set; } = new List<SchoolVacation>();
    }
}