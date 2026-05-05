﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class TeacherSupervisorSubjectsSpecification : BaseSpecification<TeacherSupervisorSubjects, long>
    {
        public TeacherSupervisorSubjectsSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public TeacherSupervisorSubjectsSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public TeacherSupervisorSubjectsSpecification(TeacherSupervisorSubjectsParams Params, bool IsInclude = true) : base(
            p =>
            (!Params.SubjectId.HasValue || p.SubjectId == Params.SubjectId)
            && (!Params.TeacherId.HasValue || p.TeacherId == Params.TeacherId)
            )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Teacher);
            Includes.Add(p => p.Subject);
        }
    }
}