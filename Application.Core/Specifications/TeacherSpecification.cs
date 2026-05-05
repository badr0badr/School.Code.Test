﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Linq;

namespace Application.Core.Specifications
{
    public class TeacherSpecification : BaseSpecification<Teacher, long>
    {
        public TeacherSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public TeacherSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public TeacherSpecification(TeacherParams Params, bool IsInclude = true) : base(
            p =>
            (string.IsNullOrEmpty(Params.Search) || p.Name == Params.Search)
            && (string.IsNullOrEmpty(Params.RoleId) || p.RoleId == Params.RoleId)
            && (!Params.SchoolId.HasValue || p.SchoolId == Params.SchoolId)
            && (!Params.MainSubjectId.HasValue || p.MainSubjectId == Params.MainSubjectId)
            && (!Params.CanAccesControl.HasValue || p.CanAccesControl == Params.CanAccesControl)
            && (!Params.HasQuiltyControl.HasValue || p.HasQuiltyControl == Params.HasQuiltyControl)
            )
        {
            AddOrderBy(p => p.Name);
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Role);
            Includes.Add(p => p.Role2);
            Includes.Add(p => p.TeacherTitle);
            Includes.Add(p => p.School);
            Includes.Add(p => p.MainSubject);
            Includes.Add(p => p.Assignments);
            Includes.Add(p => p.Classes);
        }
    }
}