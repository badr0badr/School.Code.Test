﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class ClassSubjectShareSpecification : BaseSpecification<ClassSubjectShare, long>
    {
        public ClassSubjectShareSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public ClassSubjectShareSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public ClassSubjectShareSpecification(ClassSubjectShareParams Params, bool IsInclude = true) : base(
            p =>

            (string.IsNullOrEmpty(Params.ClassName) || p.ClassName == Params.ClassName)
            && (!Params.ShareAmount.HasValue || p.ShareAmount == Params.ShareAmount)
            && (!Params.SubjectId.HasValue || p.SubjectId == Params.SubjectId)
            )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Subject);
        }
    }
}