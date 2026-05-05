﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class ClassSubjectTermWorkSpecification : BaseSpecification<ClassSubjectTermWork, long>
    {
        public ClassSubjectTermWorkSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public ClassSubjectTermWorkSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public ClassSubjectTermWorkSpecification(ClassSubjectTermWorklParams Params, bool IsInclude = true) : base(
           p =>
           (string.IsNullOrEmpty(Params.ClassName) || p.ClassName == Params.ClassName)
           && (!Params.SubjectId.HasValue || Params.SubjectId == p.SubjectId)
           && (!Params.WeekId.HasValue || Params.WeekId == p.WeekId)
           && (!Params.Month.HasValue || Params.Month == p.Week.Month)
           )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Week);
        }
    }
}