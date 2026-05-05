﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class StudentAbsentDataSpecification : BaseSpecification<StudentAbsentData, long>
    {
        public StudentAbsentDataSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public StudentAbsentDataSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public StudentAbsentDataSpecification(StudentAbsentDataParams Params, bool IsInclude = true) : base(
            p =>
            (!Params.StudentId.HasValue || Params.StudentId == p.StudentId)
            && (!Params.WeekId.HasValue || Params.WeekId == p.WeekId)
            && (!Params.TeacherId.HasValue || Params.TeacherId == p.TeacherId)
            && (!Params.ClassId.HasValue || Params.ClassId == p.ClassId)
            && (!Params.SubjectId.HasValue || Params.SubjectId == p.SubjectId)
            && (!Params.IsAbsent.HasValue || Params.IsAbsent == p.IsAbsent)
            && (!Params.Date.HasValue || Params.Date == p.Date)
            && (!Params.ClassNumber.HasValue || Params.ClassNumber == p.ClassNumber)
            )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Student);
            Includes.Add(p => p.Week);
            Includes.Add(p => p.Class);
            Includes.Add(p => p.Subject);
            Includes.Add(p => p.Teacher);
        }
    }
}