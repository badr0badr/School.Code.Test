﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class StudentExamScoreDataSpecification : BaseSpecification<StudentExamScoreData, long>
    {
        public StudentExamScoreDataSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public StudentExamScoreDataSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public StudentExamScoreDataSpecification(StudentExamScoreDataParams Params, bool IsInclude = true) : base(
            p =>
            (!Params.Month.HasValue || Params.Month == p.Month)
            && (!Params.StudentId.HasValue || Params.StudentId == p.StudentId)
            && (!Params.SubjectId.HasValue || Params.SubjectId == p.SubjectId)
            && (!Params.TeacherId.HasValue || Params.TeacherId == p.TeacherId)
            && (!Params.ClassId.HasValue || Params.ClassId == p.ClassId)
            && (!Params.IsSaved.HasValue || Params.IsSaved == p.IsSaved)
            )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Student);
            Includes.Add(p => p.Teacher);
            Includes.Add(p => p.Subject);
            Includes.Add(p => p.Class);
        }
    }
}