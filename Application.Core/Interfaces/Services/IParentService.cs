﻿using Application.Core.Views.Other;
using Application.Core.Views.Parent;
using Application.Core.Views.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IParentService
    {
        Task<StudentProfile> GetStudentProfile(long StudentId);
        Task<List<StudentPunchmentsData>> GetStudentPunchments(long StudentId);
        Task<ErrorResponce> ConfirmStudentPunchments(long PunchmentId);
        Task<ErrorResponce> SendComplent(SendComplentView view);
        Task<List<StudentComplentData>> GetStudentComplents(long StudentId);
        Task<TotalStudentAvrage> GetStudentAvrageView(long StudentId);

    }
}