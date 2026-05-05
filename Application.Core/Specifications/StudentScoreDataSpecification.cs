﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class StudentScoreDataSpecification : BaseSpecification<StudentScoreData, long>
    {
        public StudentScoreDataSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public StudentScoreDataSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public StudentScoreDataSpecification(StudentScoreDataParams Params, bool IsInclude = true) : base(
            p =>
            (!Params.WeekId.HasValue || Params.WeekId == p.WeekId)
            && (!Params.StudentId.HasValue || Params.StudentId == p.StudentId)
            && (!Params.SubjectId.HasValue || Params.SubjectId == p.SubjectId)
            && (!Params.TeacherId.HasValue || Params.TeacherId == p.TeacherId)
            && (!Params.ClassId.HasValue || Params.ClassId == p.ClassId)
            && (!Params.IsSaved.HasValue || Params.IsSaved == p.IsSaved)
            && (!Params.IsPreAbsent.HasValue || Params.IsPreAbsent == p.IsPreAbsent)
            && (!Params.Month.HasValue || Params.Month == p.Week.Month)
            )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Student);
            Includes.Add(p => p.Teacher);
            Includes.Add(p => p.Week);
            Includes.Add(p => p.Subject);
            Includes.Add(p => p.Class);
        }
    }
}