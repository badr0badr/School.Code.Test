﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class DailySupervisionSpecification : BaseSpecification<DailySupervision, long>
    {
        public DailySupervisionSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public DailySupervisionSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Class);
            Includes.Add(p => p.TSaturday);
            Includes.Add(p => p.TSunday);
            Includes.Add(p => p.TMonday);
            Includes.Add(p => p.TTuseday);
            Includes.Add(p => p.TWednesday);
            Includes.Add(p => p.TThursday);
            Includes.Add(p => p.School);
        }
    }
}