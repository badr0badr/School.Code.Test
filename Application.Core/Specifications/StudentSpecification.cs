﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Linq;

namespace Application.Core.Specifications
{
    public class StudentSpecification : BaseSpecification<Student, long>
    {
        public StudentSpecification(bool IsInclude = true)
        {
            AddOrderBy(p => p.Name);
            if (IsInclude) ApplyIncludes();
        }
        public StudentSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            AddOrderBy(p => p.Name);
            if (IsInclude) ApplyIncludes();
        }
        public StudentSpecification(StudentParams Params, bool IsInclude = true) : base(
            p =>
            (string.IsNullOrEmpty(Params.Search) || p.Name.ToLower().Contains(Params.Search))
            && (!Params.ClassId.HasValue || p.ClassId == Params.ClassId)
            && (!Params.IsDeleted.HasValue || p.IsDeleted == Params.IsDeleted)
            && (!Params.Code.HasValue || p.Code == Params.Code)
            )
        {
            AddOrderBy(p => p.Name);
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Class);
            Includes.Add(p => p.Absents);
            Includes.Add(p => p.Scores);
            Includes.Add(p => p.Exams);
        }
    }
}