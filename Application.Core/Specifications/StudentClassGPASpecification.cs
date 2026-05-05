﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class StudentClassGPASpecification : BaseSpecification<StudentClassGPA, long>
    {
        public StudentClassGPASpecification(bool IsInclude = true)
        {
            AddOrderBy(p => p.Class);
            if (IsInclude) ApplyIncludes();
        }
        public StudentClassGPASpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            AddOrderBy(p => p.Class);
            if (IsInclude) ApplyIncludes();
        }
        public StudentClassGPASpecification(StudentClassGPAParams Params, bool IsInclude = true) : base(
            p =>
            (!Params.ClassId.HasValue || p.ClassId == Params.ClassId)
            && (!Params.StudentId.HasValue || p.StudentId == Params.StudentId)
            && (!Params.WeeKId.HasValue || p.WeeKId == Params.WeeKId)
            )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Student);
            Includes.Add(p => p.Class);
            Includes.Add(p => p.Week);
        }
    }
}