﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class AdministrationsVacationSpecification : BaseSpecification<AdministrationsVacation, long>
    {
        public AdministrationsVacationSpecification(bool IsInclude = true)
        {
            AddOrderBy(p => p.StartDate);
            if (IsInclude) ApplyIncludes();
        }
        public AdministrationsVacationSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            AddOrderBy(p => p.StartDate);
            if (IsInclude) ApplyIncludes();
        }
        public AdministrationsVacationSpecification(AdministrationsVacationParams Params, bool IsInclude = true) : base(
           p =>
           (string.IsNullOrEmpty(Params.Reason) || p.Reason == Params.Reason)
           && (!Params.AdministrationId.HasValue || p.AdministrationId == Params.AdministrationId)
           && (!Params.StartDate.HasValue || p.StartDate == Params.StartDate)
           && (!Params.EndDate.HasValue || p.EndDate == Params.EndDate)
           && (!Params.Date.HasValue || (p.StartDate <= Params.Date && p.EndDate >= Params.Date))
           )
        {

            AddOrderBy(p => p.StartDate);
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Administration);
        }
    }
}