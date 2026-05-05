﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class WeekSpecification : BaseSpecification<Week, long>
    {
        public WeekSpecification() : base(p => p.IsActive == true)
        {
            AddOrderBy(p => p.Index);
        }
        public WeekSpecification(long id) : base(p => p.Id == id && p.IsActive == true)
        {
            AddOrderBy(p => p.Index);
        }
        public WeekSpecification(WeekParams Params) : base(
            p =>
            (!Params.Index.HasValue || p.Index == Params.Index)
            && (!Params.Id.HasValue || p.Id == Params.Id)
            && (!Params.Month.HasValue || p.Month == Params.Month)
            && (!Params.IsActive.HasValue || p.IsActive == Params.IsActive)
            && (!Params.EndDate.HasValue || p.EndDate == Params.EndDate)
            && (!Params.StartDate.HasValue || p.StartDate == Params.StartDate)
            && (!Params.Date.HasValue || p.StartDate <= Params.Date)
            && (!Params.Date.HasValue || p.EndDate >= Params.Date)
            )
        {
            AddOrderBy(p => p.Index);
        }
    }
}