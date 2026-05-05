﻿using Application.Core.Views.Adminstration;
using Application.Core.Views.Directorate;
using Application.Core.Views.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces.Services
{
    public interface IAdminstrationService
    {
        Task<ErrorResponce> AddAdminstrationUser(AddAgencyUserView view);
        Task<ErrorResponce> TransferTeacher(TransferTeacherView view);
        Task<ErrorResponce> AssignmentTeacherToSchool(AssignTeacherToSchoolView view);
        Task<ErrorResponce> ConfirmStudentTransfer(long TransformationId);
    }
}