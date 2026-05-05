﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Linq;

namespace Application.Core.Specifications
{
    public class TeacherClassesSpecification : BaseSpecification<TeacherClasses, long>
    {
        public TeacherClassesSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public TeacherClassesSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public TeacherClassesSpecification(TeacherClassesParams Params, bool IsInclude = true) : base(
            p =>
            (!Params.TeacherId.HasValue || p.TeacherId == Params.TeacherId)
            && (!Params.ClassId.HasValue || p.ClassId == Params.ClassId)
            && (!Params.SubjectId.HasValue || Params.SubjectId == p.SubjectId)
            && (!Params.SchoolId.HasValue || Params.SchoolId == p.Class.SchoolId)
            && (string.IsNullOrEmpty(Params.ClassName) || p.Class.Name == Params.ClassName)
            )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Class);
            Includes.Add(p => p.Teacher);
            Includes.Add(p => p.Subject);
        }
    }
}