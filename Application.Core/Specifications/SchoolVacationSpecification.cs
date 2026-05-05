﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class SchoolVacationSpecification : BaseSpecification<SchoolVacation, long>
    {
        public SchoolVacationSpecification(bool IsInclude = true)
        {
            AddOrderBy(p => p.StartDate);
            if (IsInclude) ApplyIncludes();
        }
        public SchoolVacationSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            AddOrderBy(p => p.StartDate);
            if (IsInclude) ApplyIncludes();
        }
        public SchoolVacationSpecification(SchoolVacationParams Params, bool IsInclude = true) : base(
            p =>
            (!Params.SchoolId.HasValue || Params.SchoolId == p.SchoolId)
            )
        {
            AddOrderBy(p => p.StartDate);
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.School);
        }
    }
}