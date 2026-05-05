﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class ExamCodesSpecification : BaseSpecification<ExamCodes, long>
    {
        public ExamCodesSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public ExamCodesSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public ExamCodesSpecification(ExamCodesParams Params, bool IsInclude = true) : base(
           p =>
           (!Params.StudentId.HasValue || Params.StudentId == p.StudentId)
           && (!Params.SubjectId.HasValue || Params.SubjectId == p.SubjectId)
           && (!Params.Code.HasValue || Params.Code == p.Code)
           )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Student);
            Includes.Add(p => p.Subject);
        }
    }
}