﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class GeneralSupervisionSpecification : BaseSpecification<GeneralSupervision, long>
    {
        public GeneralSupervisionSpecification(bool IsInclude = true)
        {
            if (IsInclude) ApplyIncludes();
        }
        public GeneralSupervisionSpecification(long id, bool IsInclude = true) : base(p => p.Id == id)
        {
            if (IsInclude) ApplyIncludes();
        }
        private void ApplyIncludes()
        {
            Includes.Add(p => p.Teacher);
            Includes.Add(p => p.School);
        }
    }
}