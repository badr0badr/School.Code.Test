﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class SubjectSpecification : BaseSpecification<Subject, long>
    {
        public SubjectSpecification(bool IsInclude = true)
        {
            AddOrderBy(p => p.Index);
            if (IsInclude) ApplyIncludes();
        }
        public SubjectSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            AddOrderBy(p => p.Index);
            if (IsInclude) ApplyIncludes();
        }
        public SubjectSpecification(SubjectParams Params, bool IsInclude = true) : base(
            p =>
            (string.IsNullOrEmpty(Params.NotStatus) || p.Status != Params.NotStatus)
            && (string.IsNullOrEmpty(Params.Status) || p.Status == Params.Status)
            )
        {
            AddOrderBy(p => p.Index);
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.ClassSubjectShares);
        }
    }
}