﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class AssignmentSpecification : BaseSpecification<Assignment, long>
    {
        public AssignmentSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public AssignmentSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public AssignmentSpecification(AssignmentParams Params, bool IsInclude = true) : base(
            p =>
            (!Params.TeacherId.HasValue || p.TeacherId == Params.TeacherId)
            && (!Params.SchoolId.HasValue || p.SchoolId == Params.SchoolId)
            )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Teacher);
            Includes.Add(p => p.School);
        }
    }
}