﻿using Application.Core.Views.Auth;
using Application.Core.Views.Other;
using System;
using System.Linq;

namespace Application.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TeacherData> TeacherLogin(LoginView login);
        Task<TeacherData> TeacherDataByToken(string Id);
        Task<ErrorResponce> Signin(SigninView view);
        Task<PerantData> PerantLogin(long Code);
        Task<PerantData> PerantDataByToken(string Id);
        Task<GetProfileView> GetProfile(long Id);
        Task<TeacherData> ControlTeacherDataByToken(string Id);
        Task<TeacherData> ControlTeacherLogin(LoginView login);
    }
}