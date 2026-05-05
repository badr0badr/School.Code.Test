﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class StudentAbsentCasesSpecification : BaseSpecification<StudentAbsentCases, long>
    {
        public StudentAbsentCasesSpecification(bool IsInclude = true)

        {
            //AddOrderBy(p => p.Name);
            if (IsInclude) ApplyIncludes();
        }
        public StudentAbsentCasesSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            //AddOrderBy(p => p.Name);
            if (IsInclude) ApplyIncludes();
        }
        public StudentAbsentCasesSpecification(StudentAbsentCasesParams Params, bool IsInclude = true) : base(
            p =>
            (string.IsNullOrEmpty(Params.Reason) || p.Reason.ToLower().Contains(Params.Reason))
            && (!Params.StudentId.HasValue || p.StudentId == Params.StudentId)
            && (!Params.EndDate.HasValue || p.EndDate == Params.EndDate)
            && (!Params.StartDate.HasValue || p.StartDate == Params.StartDate)
            && (!Params.Date.HasValue || p.StartDate <= Params.Date)
            && (!Params.Date.HasValue || p.EndDate >= Params.Date)
            )
        {
            //AddOrderBy(p => p.Name);
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Student);
        }
    }
}