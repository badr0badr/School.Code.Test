﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class TeacherSupervisorSpecification : BaseSpecification<TeacherSupervisor, long>
    {
        public TeacherSupervisorSpecification(bool IsInclude = true)
        {
            ApplyIncludes();
            if (IsInclude) AddOrderBy(p => p.Id);
        }
        public TeacherSupervisorSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            ApplyIncludes();
            if (IsInclude) AddOrderBy(p => p.Id);
        }
        public TeacherSupervisorSpecification(TeacherSupervisorParams Params, bool IsInclude = true) : base(
            p =>
            (!Params.TeacherId.HasValue || Params.TeacherId == p.TeacherId)
            && (!Params.SchoolId.HasValue || Params.SchoolId == p.SchoolId)
            && (!Params.SubjectId.HasValue || Params.SubjectId == p.SubjectId)
            && (string.IsNullOrEmpty(Params.Stage) || p.Stage == Params.Stage)
            )
        {
            AddOrderBy(p => p.Id);
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Teacher);
            Includes.Add(p => p.School);
            Includes.Add(p => p.Subject);
            Includes.Add(p => p.SubSubjects);
        }
    }
}