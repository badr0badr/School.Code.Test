﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class AdministrationsSpecification : BaseSpecification<Administrations, long>
    {
        public AdministrationsSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public AdministrationsSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        public AdministrationsSpecification(AdministrationsParams Params, bool IsInclude = true) : base(
            p =>
            (string.IsNullOrEmpty(Params.Name) || p.Name == Params.Name)
            )
        {
            AddOrderBy(p => p.Name);
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Schools);
            Includes.Add(p => p.Vacations);
        }
    }
}