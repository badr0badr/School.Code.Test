﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class SchoolSpecification : BaseSpecification<School, long>
    {
        public SchoolSpecification(bool IsInclude = true)
        {
            AddOrderBy(p => p.Name);
            if (IsInclude) ApplyIncludes();
        }
        public SchoolSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            AddOrderBy(p => p.Name);
            if (IsInclude) ApplyIncludes();
        }
        public SchoolSpecification(SchoolParams Params, bool IsInclude = true) : base(
           p =>
           (string.IsNullOrEmpty(Params.Search) || p.Name.Contains(Params.Search))
           && (string.IsNullOrEmpty(Params.Name) || p.Name == Params.Name)
           && (!Params.AdministrationId.HasValue || Params.AdministrationId == p.AdministrationId)
           && (!Params.SchoolMangerId.HasValue || Params.SchoolMangerId == p.SchoolMangerId)
           )
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Administration);
            Includes.Add(p => p.Teachers);
            Includes.Add(p => p.Classes);
            Includes.Add(p => p.Assignments);
            Includes.Add(p => p.Vacations);
        }
    }
}