﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Linq;

namespace Application.Core.Specifications
{
    public class ClassesSpecification : BaseSpecification<Classes, long>
    {
        public ClassesSpecification(bool IsInclude = true)
        {
            AddOrderBy(p => p.Class);
            if (IsInclude) ApplyIncludes();
        }
        public ClassesSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            AddOrderBy(p => p.Class);
            if (IsInclude) ApplyIncludes();
        }
        public ClassesSpecification(ClassesParams Params, bool IsInclude = true) : base(
            p =>
            (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search))
            && (string.IsNullOrEmpty(Params.FullId) || p.FullId == Params.FullId)
            && (string.IsNullOrEmpty(Params.Name) || p.Name == Params.Name)
            && (string.IsNullOrEmpty(Params.Grade) || p.FullId == Params.Grade)
            && (string.IsNullOrEmpty(Params.Class) || p.FullId == Params.Class)
            && (!Params.SchoolId.HasValue || p.SchoolId == Params.SchoolId)
            && (!Params.ClassNumber.HasValue || p.ClassNumber == Params.ClassNumber)
            )
        {
            AddOrderBy(p => p.Grade);
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Students);
            Includes.Add(p => p.School);
        }
    }
}