﻿using Application.Core.Views.Directorate;
using Application.Core.Views.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IDirectorateService
    {
        Task<ErrorResponce> AddAgency(AddAgencyView view);
        Task<ErrorResponce> AddDirectorateUser(AddAgencyUserView view);
    }
}