﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class TeacherTitleSpecification : BaseSpecification<TeacherTitle, long>
    {
        public TeacherTitleSpecification()
        {
            AddOrderBy(p => p.Index);
        }
        public TeacherTitleSpecification(long id) : base(p => p.Id == id)
        {
        }
    }
}