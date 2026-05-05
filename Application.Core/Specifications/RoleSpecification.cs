﻿using Application.Core.Entities;
using Application.Core.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Specifications
{
    public class RoleSpecification : BaseSpecification<Role, string>
    {
        public RoleSpecification()
        {
        }
        public RoleSpecification(string id) : base(p => p.Id == id)
        {
        }
        public RoleSpecification(RoleParams Params) : base(
            p =>
            (string.IsNullOrEmpty(Params.Id) || p.Id == Params.Id)
            && (!Params.SchoolSelection.HasValue || Params.SchoolSelection == p.SchoolSelection)
            && (!Params.AdminSelection.HasValue || Params.AdminSelection == p.AdminSelection)
            && (!Params.DirectorSelection.HasValue || Params.DirectorSelection == p.DirectorSelection)
            )
        {
        }
    }
}